using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TemplateCodeGenerator.SchemaRead {

	abstract public class SchemaReader {

		protected SchemaReader(ICrossGeneratorNameHandler nameHandler) {
			NameHandler = nameHandler;
		}

		protected struct ColumnTypeOverride {
			public string Table { get; set; }
			public string Column { get; set; }
			public string Type { get; set; }

		}

		protected struct IgnoredColumn {
			public string Table { get; set; }
			public string Column { get; set; }
		}

		protected struct EnumMapping {
			public string TableColumnName { get; set; }
			public string EnumName { get; set; }

		}
		/// <summary>
		/// the reader will exclude/include tables as follows:
		///		1) if there are included tables indicated and the table is not in the included list it will be excluded.
		///		2) if there are excluded tables indicated and the table name macthes it will be excluded.
		///		3) if there are excluded table prefixes indicated tha the table starts with one of them it will be excluded.
		///	so if there are no included table listed any table not explicitly excluded will be included,
		///	if there are specifically included tables listed and the name of the table is not amongst them it will be excluded
		/// </summary>
		protected List<string> _ExcludedTables = new List<string>();
		protected List<string> _ExcludedTablePrefixes = new List<string>();
		protected List<string> _TableStripStrings = new List<string>();
		protected List<string> _IncludedTables = new List<string>();
		protected List<string> _AlwaysIgnoredColumns = new List<string>();
		protected List<EnumMapping> _EnumMappings = new List<EnumMapping>();
		protected List<IgnoredColumn> _IgnoredColumns = new List<IgnoredColumn>();
		protected List<TableKeys> _TableKeyColumns = new List<TableKeys>();
		protected List<ColumnTypeOverride> _ColumnTypeOverrides = new List<ColumnTypeOverride>();

		public abstract Tables GetTables();

		public ICrossGeneratorNameHandler NameHandler { get; }

		public string GeneratedClassPrefix { get; set; } = "";
		public string GeneratedClassSuffix { get; set; } = "";
		public bool SingularizeTableNames { get; set; } = true;
		public bool IncludeViews { get; set; }
		public string SchemaName { get; set; }

		public string DecorateClassName(string className) {
			return $"{GeneratedClassPrefix}{className}{GeneratedClassSuffix}";
		}

		public void AddExcludedTablePrefix(string prefix) {
			_ExcludedTablePrefixes.Add(prefix);
		}

		public void AddTableStripString(string stringToStrip) {
			_TableStripStrings.Add(stringToStrip);
		}

		public void AddExcludedTable(string tableName) {
			_ExcludedTables.Add(tableName);
		}

		public void AddExcludedTables(IEnumerable<string> tableNames) {
			_ExcludedTables.AddRange(tableNames);
		}

		public void AddAlwaysIgnoredColumn(string columnName) {
			if (!_AlwaysIgnoredColumns.Any(c => string.Equals(c, columnName, StringComparison.CurrentCultureIgnoreCase))) {
				_AlwaysIgnoredColumns.Add(columnName);
			}
		}

		public void IgnoreColumn(string tableName, string columnName) {
			_IgnoredColumns.Add(new IgnoredColumn() { Table = tableName, Column = columnName });
		}

		public void AddEnumMapping(string tablecolumnName, string enumName) {
			if (_EnumMappings.Any(e => string.Equals(tablecolumnName, e.TableColumnName, StringComparison.OrdinalIgnoreCase))) {
				throw new ArgumentException("Cannot add more than one enum mapping to the same column ");
			}
			_EnumMappings.Add(new EnumMapping() { TableColumnName = tablecolumnName, EnumName = enumName });
		}

		public void AddIncludedTable(string tableName) {
			_IncludedTables.Add(tableName);
		}

		public void AddIncludedTables(IEnumerable<string> tableNames) {
			_IncludedTables.AddRange(tableNames);
		}

		public void AddColumnTypeOverride(string table, string column, string overrideType) {
			_ColumnTypeOverrides.Add(new ColumnTypeOverride() { Table = table, Column = column, Type = overrideType });
		}

		public bool IsExcludedTable(string tablename) {
			return _ExcludedTables.Contains(tablename) || _ExcludedTablePrefixes.Any(n => tablename.StartsWith(n));
		}

		public bool IsIncludedTable(string tableName) {
			return (_IncludedTables.Count == 0 || _IncludedTables.Contains(tableName));
		}

		public bool IsIgnoredColumn(string tableName, string columnName) {
			return _IgnoredColumns.Contains(new IgnoredColumn() { Table = tableName, Column = columnName })
				|| _AlwaysIgnoredColumns.Any(i => string.Equals(i, columnName, StringComparison.CurrentCultureIgnoreCase));
		}

		public bool IsKeyColumn(string tableName, string columnName) {
			return _TableKeyColumns.Any(v => string.Equals(tableName, v.TableName) && v.Keys.Contains(columnName));
		}

		public bool IncludedSchema(string schemaName) {
			return string.IsNullOrWhiteSpace(SchemaName) || string.Equals(schemaName, SchemaName, StringComparison.CurrentCultureIgnoreCase);
		}

		public string TryGetColumnTypeMapping(string table, string column) {
			ColumnTypeOverride? typeOverride = _ColumnTypeOverrides.FirstOrDefault(o => string.Equals(o.Table, table) && string.Equals(o.Column, column));
			return typeOverride.HasValue ? typeOverride.Value.Type : string.Empty;
		}

		public string GetStrippedTableName(string tableIn) {
			string ret = tableIn;

			foreach (var match in _TableStripStrings) {
				ret = ret.Replace(match, "");
			}

			return ret;
		}

		protected string SpaceStripAndCamelCase(string inValue, bool makeLowerCamel) {

			if (!string.IsNullOrWhiteSpace(inValue)) {
				var working = inValue;
				if (!char.IsLetter(working[0])) {
					working = $"_{working}";
				}
				Regex charEx = new Regex(@"[&@$\-.,*;?{}\[\]()%/""]");
				var valuePieces = charEx.Replace(working, " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				var ret = string.Empty;
				if (makeLowerCamel) {
					Span<char> field = new Span<char>(valuePieces[0].ToCharArray());
					field[0] = char.ToLower(valuePieces[0][0]);
					ret = $"{ret}{new string(field.ToArray())}";
				}
				foreach (var piece in valuePieces.Skip(makeLowerCamel ? 1 : 0)) {
					Span<char> field = new Span<char>(piece.ToCharArray());
					field[0] = char.ToUpper(piece[0]);
					ret = $"{ret}{new string(field.ToArray())}";
				}
				return ret;
			}
			return inValue;
		}

		/// <summary>
		/// Determines if a named C# property type should be emitted with a nullable indicator when null
		/// storage is detected.
		/// </summary>
		/// <param name="propertyType">property type string name to be evaluated</param>
		/// <returns>boolean indicating if a null indicator should be applied when dealing with null values</returns>
		protected bool RequiresNullableIndicator(string propertyType) {
			return !(string.Compare(propertyType, "object", true) == 0
					|| string.Compare(propertyType, "string", true) == 0
					|| propertyType.EndsWith("[]"));
		}

		/// <summary>
		/// Translate a data storage type string to the appropriate c# data storage type string.
		/// </summary>
		/// <param name="sqlType">type name to be translated</param>
		/// <returns>translated type name</returns>
		protected string GetPropertyType(string sqlType) {
			string sysType = "object";
			switch (sqlType.Replace("[", "").Replace("]", "").ToLower()) {
				case "char":
				case "varchar":
				case "nchar":
				case "nvarchar":
				case "text":
					sysType = "string";
					break;
				case "bigint":
					sysType = "long";
					break;
				case "smallint":
					sysType = "short";
					break;
				case "int":
					sysType = "int";
					break;
				case "uniqueidentifier":
					sysType = "Guid";
					break;
				case "smalldatetime":
				case "datetime":
				case "datetime2":
				case "date":
				case "time":
					sysType = "DateTime";
					break;
				case "float":
					sysType = "double";
					break;
				case "real":
					sysType = "float";
					break;
				case "numeric":
				case "smallmoney":
				case "decimal":
				case "money":
					sysType = "decimal";
					break;
				case "tinyint":
					sysType = "byte";
					break;
				case "bit":
					sysType = "bool";
					break;
				case "image":
				case "binary":
				case "varbinary":
				case "timestamp":
					sysType = "byte[]";
					break;
				case "geography":
					sysType = "Microsoft.SqlServer.Types.SqlGeography";
					break;
				case "geometry":
					sysType = "Microsoft.SqlServer.Types.SqlGeometry";
					break;
				case "xml":
					sysType = "string";
					break;
			}

			return sysType;
		}

		/// <summary>
		/// Add length/precision/scale to appropriate data types.
		/// </summary>
		/// <param name="dataTypeName"></param>
		/// <param name="precision"></param>
		/// <param name="scale"></param>
		/// <param name="length"></param>
		/// <returns> decorated datatype definition for appropriate types or the passed in type. /returns>
		protected string DecorateDataType(string dataTypeName, int precision, int scale, int length) {
			string ret = dataTypeName;
			switch (dataTypeName.Replace("[", "").Replace("]", "").ToLower()) {
				case "numeric":
				case "decimal":
					ret = $"{dataTypeName}({precision}, {scale})";
					break;
				case "char":
				case "varchar":
				case "nchar":
				case "nvarchar":
				case "binary":
				case "varbinary":
					if (length == -1) {
						ret = $"{dataTypeName}(max)";
					}
					else {
						ret = $"{dataTypeName}({length})";
					}

					break;
				case "text":
					ret = $"[varchar](max)";
					break;
				case "image":
					ret = $"[binary](max)";
					break;

			}
			return ret;
		}

	}

}
