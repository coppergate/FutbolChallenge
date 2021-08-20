using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace TemplateCodeGenerator.SchemaRead {
	public class SqlServerSchemaReader : SchemaReader {
		DbConnection _connection;
		DbProviderFactory _factory;
		public ITextTemplatingEngineHost Host { get; set; }

		public string ProviderName { get; set; }

		public string ConnectionStringName { get; set; }
		public string ConnectionString { get; set; }

		readonly ConnectionStringProvider _Provider;

		public SqlServerSchemaReader(ICrossGeneratorNameHandler nameHandler) : base(nameHandler) {
			_Provider = new ConnectionStringProvider(ConnectionStringName, Host);
			ConnectionString = _Provider.ConnectionString;
			ProviderName = _Provider.ProviderName;
		}

		public SqlServerSchemaReader(string connectionString, string providerName, ICrossGeneratorNameHandler nameHandler) : base(nameHandler) {
			ConnectionString = connectionString;
			ProviderName = providerName;
		}

		~SqlServerSchemaReader() {
		}

		override public Tables GetTables() {

			try {
				_factory = DbProviderFactories.GetFactory(ProviderName);
			}
			catch (Exception) {
				return new Tables();
			}

			try {
				Tables result;
				using (var conn = _factory.CreateConnection()) {
					conn.ConnectionString = ConnectionString;
					conn.Open();

					result = ReadSchema(conn, _factory);

					List<string> tablesToRemove = new List<string>();
					// Remove unrequired tables/views
					foreach (Table t in result.All) {
						if (SchemaName != null && string.Compare(t.Schema, SchemaName, true) != 0) {
							tablesToRemove.Add(t.SchemaQualifiedName);
							continue;
						}
						if (!IncludeViews && t.IsView) {
							tablesToRemove.Add(t.SchemaQualifiedName);
							continue;
						}
						if (t.IsView) {
							t.MakeReadOnly = true;
						}
					}
					result.RemoveAll(tablesToRemove);

					conn.Close();

					var rxClean = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
					foreach (var t in result.All) {
						t.ClassName = $"{GeneratedClassPrefix}{t.ClassName}{GeneratedClassSuffix}";
						foreach (var c in t.Columns) {
							c.PropertyName = rxClean.Replace(c.PropertyName, "_$1");

							// Make sure property name doesn't clash with class name
							if (c.PropertyName == t.ClassName) {
								c.PropertyName = "_" + c.PropertyName;
							}
						}
					}
					return result;
				}
			}
			catch (Exception x) {
				var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
				//WriteLine("// FAILED in LoadTables");
				//WriteLine(error);
				return new Tables();
			}
		}

		protected Tables ReadSchema(DbConnection connection, DbProviderFactory factory) {
			var result = new Tables();

			_connection = connection;
			_factory = factory;

			var cmd = _factory.CreateCommand();
			cmd.Connection = connection;
			cmd.CommandText = TABLE_SQL;

			//pull the tables in a reader
			using (cmd) {

				using (var rdr = cmd.ExecuteReader()) {
					while (rdr.Read()) {
						string tableName = rdr["TABLE_NAME"].ToString();
						if (!IsIncludedTable(tableName) || IsExcludedTable(tableName)) {
							continue;
						}

						Table tbl = new Table();
						tbl.Name = tableName;
						tbl.Schema = rdr["TABLE_SCHEMA"].ToString();
						tbl.IsView = string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true) == 0;
						tbl.CleanName = ObjectNameHandler.CleanForUseInGeneratedCode(tbl.Name, SingularizeTableNames);
						if (tbl.CleanName.StartsWith("tbl_")) {
							tbl.CleanName = tbl.CleanName.Replace("tbl_", "");
						}

						if (tbl.CleanName.StartsWith("tbl")) {
							tbl.CleanName = tbl.CleanName.Replace("tbl", "");
						}

						tbl.ClassName = tbl.CleanName.Replace("_", "");

						result.Add(tbl);
					}
				}
			}

			foreach (var tbl in result.All) {
				tbl.Columns = LoadColumns(tbl);

				// Mark the primary key
				List<string> pks = GetPK(tbl.Name, tbl.Schema);
				foreach (var pkColumn in tbl.Columns) {
					pkColumn.IsPK = pks.Any(x => string.Compare(x, pkColumn.Name.Trim(), true) == 0);
				}

				try {
					tbl.OuterKeys = LoadOuterKeys(tbl);
					tbl.InnerKeys = LoadInnerKeys(tbl);
				}
				catch (Exception x) {
					var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
					throw new Exception($"// Failed to get relationships for {tbl.Name} - {error}");
				}
				tbl.BuildSql();
			}

			return result;
		}

		List<Column> LoadColumns(Table tbl) {
			using (var cmd = _factory.CreateCommand()) {
				cmd.Connection = _connection;
				cmd.CommandText = COLUMN_SQL;

				var p = cmd.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = tbl.Name;
				cmd.Parameters.Add(p);

				p = cmd.CreateParameter();
				p.ParameterName = "@schemaName";
				p.Value = tbl.Schema;
				cmd.Parameters.Add(p);

				var result = new List<Column>();
				using (IDataReader rdr = cmd.ExecuteReader()) {
					while (rdr.Read()) {
						string colName = rdr["ColumnName"].ToString();
						if (IsIgnoredColumn(tbl.Name, colName)) {
							continue;
						}

						Column col = new Column();
						col.Name = colName;
						col.PropertyName = ObjectNameHandler.CleanForUseInGeneratedCode(col.Name, false);
						col.PropertyType = GetPropertyType(rdr["DataType"].ToString());

						col.MaximumLength = Convert.ToInt32(rdr["MaxLength"]);
						col.NumericPrecision = Convert.ToInt32(rdr["NumericPrecision"]);
						col.NumericScale = Convert.ToInt32(rdr["NumericScale"]);

						col.SqlDataType = DecorateDataType(rdr["DataType"].ToString(), col.NumericPrecision, col.NumericScale, col.MaximumLength);


						bool nullableField = rdr["IsNullable"].ToString() == "YES";
						string propertyType = col.PropertyType;

						col.IsNullable = nullableField;
						col.RequiresNullableIndicator = RequiresNullableIndicator(propertyType);
						col.IsAutoIncrement = ((int)rdr["IsIdentity"]) == 1;
						col.IsPK = ((int)rdr["IsPk"]) == 1;
						result.Add(col);
					}
				}

				return result;
			}
		}

		private bool RequiresNullableIndicator(string propertyType) {
			return !(string.Compare(propertyType, "string", true) == 0
					|| string.Compare(propertyType, "xml", true) == 0
					|| propertyType.EndsWith("[]"));
		}


		//	NB: I believe these key adds are incorrect.
		//		the process should be to find a foreign key and then 
		//		add all the columns to the referenced/referencer lists
		List<Key> LoadOuterKeys(Table tbl) {
			using (var cmd = _factory.CreateCommand()) {
				cmd.Connection = _connection;
				cmd.CommandText = OUTER_KEYS_SQL;

				var p = cmd.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = $"{tbl.Schema}.{tbl.Name}";
				cmd.Parameters.Add(p);

				var result = new List<Key>();
				using (IDataReader rdr = cmd.ExecuteReader()) {
					while (rdr.Read()) {
						var key = new Key();
						key.Name = rdr["FK"].ToString();
						key.ReferencedTableName = rdr["Referenced_tbl"].ToString();
						key.ReferencedTableColumnNames.Add(rdr["Referenced_col"].ToString());
						key.ReferencingTableName = rdr["Referencing_tbl"].ToString();
						key.ReferencingTableColumnNames.Add(rdr["Referencing_col"].ToString());
						result.Add(key);
					}
				}

				return result;
			}
		}

		List<Key> LoadInnerKeys(Table tbl) {
			using (var cmd = _factory.CreateCommand()) {
				cmd.Connection = _connection;
				cmd.CommandText = INNER_KEYS_SQL;

				var p = cmd.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = $"{tbl.Schema}.{tbl.Name}";
				cmd.Parameters.Add(p);

				var result = new List<Key>();
				using (IDataReader rdr = cmd.ExecuteReader()) {
					while (rdr.Read()) {
						var key = new Key();
						key.Name = rdr["FK"].ToString();
						key.ReferencingTableName = rdr["Referencing_tbl"].ToString();
						key.ReferencedTableColumnNames.Add(rdr["Referenced_col"].ToString());
						key.ReferencingTableColumnNames.Add(rdr["Referencing_col"].ToString());
						result.Add(key);
					}
				}

				return result;
			}
		}

		List<string> GetPK(string table, string schema) {
			List<string> pks = new List<string>();
			string sql = PK_SQL;

			using (var cmd = _factory.CreateCommand()) {
				cmd.Connection = _connection;
				cmd.CommandText = sql;

				var p = cmd.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = table;
				cmd.Parameters.Add(p);

				p = cmd.CreateParameter();
				p.ParameterName = "@schema";
				p.Value = schema;
				cmd.Parameters.Add(p);

				using (IDataReader result = cmd.ExecuteReader()) {
					while (result.Read()) {
						pks.Add(result[2].ToString().ToUpper().Trim());
					}
				}
			}

			return pks;
		}



		const string TABLE_SQL = @"SELECT *
		FROM  INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'";

		const string COLUMN_SQL = @"SELECT DISTINCT			
    c.TABLE_CATALOG AS [Database],
    c.TABLE_SCHEMA AS Owner, 
    c.TABLE_NAME AS TableName, 
    c.COLUMN_NAME AS ColumnName, 
    c.ORDINAL_POSITION AS OrdinalPosition, 
    c.COLUMN_DEFAULT AS DefaultSetting, 
    c.IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, 
    IsNull(c.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, 
    IsNull(c.DATETIME_PRECISION, 0) AS DatePrecision,
    IsNull(c.NUMERIC_PRECISION, 0) AS NumericPrecision,
    IsNull(c.NUMERIC_SCALE, 0) AS NumericScale,
    COLUMNPROPERTY(object_id('[' + c.TABLE_SCHEMA + '].[' + c.TABLE_NAME + ']'), c.COLUMN_NAME, 'IsIdentity') AS IsIdentity,
    COLUMNPROPERTY(object_id('[' + c.TABLE_SCHEMA + '].[' + c.TABLE_NAME + ']'), c.COLUMN_NAME, 'IsComputed') as IsComputed,
    case when k.COLUMN_NAME IS NULL THEN 0 ELSE 1 end AS IsPk from 
    INFORMATION_SCHEMA.COLUMNS c LEFT JOIN 
	INFORMATION_SCHEMA.KEY_COLUMN_USAGE k
	on c.TABLE_NAME = k.TABLE_NAME
	AND c.COLUMN_NAME = k.COLUMN_NAME
		WHERE c.TABLE_NAME=@tableName AND c.TABLE_SCHEMA=@schemaName;";

		const string OUTER_KEYS_SQL = @"SELECT 
			FK = OBJECT_NAME(pt.constraint_object_id),
			Referenced_tbl = OBJECT_NAME(pt.referenced_object_id),
			Referencing_tbl = OBJECT_NAME(pt.parent_object_id), 
			Referencing_col = pc.name, 
			Referenced_col = rc.name
		FROM sys.foreign_key_columns AS pt
		INNER JOIN sys.columns AS pc
		ON pt.parent_object_id = pc.[object_id]
		AND pt.parent_column_id = pc.column_id
		INNER JOIN sys.columns AS rc
		ON pt.referenced_column_id = rc.column_id
		AND pt.referenced_object_id = rc.[object_id]
		WHERE pt.parent_object_id = OBJECT_ID(@tableName);";

		const string PK_SQL = @"select c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS as c
inner join INFORMATION_SCHEMA.TABLE_CONSTRAINTS as tc
on c.TABLE_NAME = tc.TABLE_NAME
and c.TABLE_SCHEMA = tc.TABLE_SCHEMA
inner join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE as cu
on cu.TABLE_SCHEMA = c.TABLE_SCHEMA
and cu.TABLE_NAME= c.TABLE_NAME
and cu.COLUMN_NAME = c.COLUMN_NAME 
and cu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
where  tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
and c.TABLE_NAME=@tableName AND c.TABLE_SCHEMA= @schema
";

		const string INNER_KEYS_SQL = @"SELECT 
			[Schema] = OBJECT_SCHEMA_NAME(pt.parent_object_id),
			Referencing_tbl = OBJECT_NAME(pt.parent_object_id),
			FK = OBJECT_NAME(pt.constraint_object_id),
			Referencing_col = pc.name, 
			Referenced_col = rc.name
		FROM sys.foreign_key_columns AS pt
		INNER JOIN sys.columns AS pc
		ON pt.parent_object_id = pc.[object_id]
		AND pt.parent_column_id = pc.column_id
		INNER JOIN sys.columns AS rc
		ON pt.referenced_column_id = rc.column_id
		AND pt.referenced_object_id = rc.[object_id]
		WHERE pt.referenced_object_id = OBJECT_ID(@tableName);";
	}


}
