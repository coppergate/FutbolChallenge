using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TemplateCode.Generators.Properties;

namespace TemplateCodeGenerator.SchemaRead {

	using SqlColumn = Microsoft.SqlServer.Dac.Model.Column;
	using SqlTable = Microsoft.SqlServer.Dac.Model.Table;

	internal class TableColumnDescription {

		internal TableColumnDescription(string tableName, string columnName, string description) {
			TableName = tableName;
			ColumnName = columnName;
			Description = description;
		}

		public string TableName { get; private set; }
		public string ColumnName { get; private set; }
		public string Description { get; private set; }

	}

	public class DacPacSchemaReader : SchemaReader {

		public string DacPacResourceName { get; set; }
		private readonly List<TableColumnDescription> ColumnDescription = new List<TableColumnDescription>();

		public DacPacSchemaReader(string dacPacPath, ICrossGeneratorNameHandler nameHandler) : base(nameHandler) {
			DacPacResourceName = dacPacPath;
		}

		override public Tables GetTables() {

			try {
				Tables tables = new Tables();
				using (Stream dacPac = new MemoryStream((byte[])Resources.ResourceManager.GetObject(DacPacResourceName)))
				using (DacPackage package = DacPackage.Load(dacPac, DacSchemaModelStorageType.Memory))
				using (TSqlModel sqlModel = TSqlModel.LoadFromDacpac(dacPac, new ModelLoadOptions() { LoadAsScriptBackedModel = false, ModelStorageType = DacSchemaModelStorageType.Memory, ThrowOnModelErrors = false })) {
					var objectSchemaType = SqlTable.Schema;

					GetAllExtendedProperties(sqlModel);

					foreach (TSqlObject obj in sqlModel.GetObjects(DacQueryScopes.UserDefined, SqlTable.TypeClass, View.TypeClass)) {

						if (obj.ObjectType == View.TypeClass) {
							objectSchemaType = View.Schema;
						}
						var schemaName = GetBaseObjectName(obj.GetReferencedRelationshipInstances(objectSchemaType).First().ObjectName);

						if (!IncludedSchema(schemaName)) {
							continue;
						}
						if (!IncludeViews && obj.ObjectType == View.TypeClass) {
							continue;
						}

						var schemaQualifiedName = $"[{schemaName}].[{ GetBaseObjectName(obj.Name)}]"; ;
						if (!IsIncludedTable(schemaQualifiedName) || IsExcludedTable(schemaQualifiedName)) {
							continue;
						}

						Table table = CreateTable(obj, schemaName);

						if (obj.ObjectType == View.TypeClass) {
							table.MakeReadOnly = true;
							table.IsView = true;
						}

						tables.Add(table);
					}

					LoadReferenceKeys(tables, sqlModel);
				}
				return tables;
			}
			catch (Exception x) {
				var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
				return new Tables();
			}

		}


		private void GetAllExtendedProperties(TSqlModel sqlModel) {

			var extendedProperties = sqlModel.GetObjects(DacQueryScopes.All, ExtendedProperty.TypeClass);

			foreach (var prop in extendedProperties) {
				if (prop.Name.Parts[4] == "MS_Description") {
					var parentName = prop.GetParent().Name;
					var value = (string)prop.GetProperty(ExtendedProperty.Value);
					value = value.Substring(2, value.Length - 3);
					TableColumnDescription description = new TableColumnDescription(parentName.Parts[1], parentName.Parts[2], value);
					ColumnDescription.Add(description);
				}
			}

		}

		private void LoadReferenceKeys(Tables tables, TSqlModel sqlModel) {
			foreach (var foreignKey in sqlModel.GetObjects(DacQueryScopes.All, ForeignKeyConstraint.TypeClass)) {
				var foreignTable = foreignKey.GetReferenced(ForeignKeyConstraint.ForeignTable).First().Name;
				var foreignColumns = foreignKey.GetReferenced(ForeignKeyConstraint.ForeignColumns);
				var definingTable = foreignKey.GetReferenced(ForeignKeyConstraint.Host).First().Name;
				var definingTableColumns = foreignKey.GetReferenced(ForeignKeyConstraint.Columns);
				//	Add the reference from the defining table to the referenced table
				if (tables.ContainsTable(definingTable.ToString())) {
					Table fromTable = tables[definingTable.ToString()];
					fromTable.OuterKeys.Add(new Key() {
						Name = foreignKey.Name.ToString(),
						ReferencingTableName = fromTable.SchemaQualifiedName,
						ReferencingTableColumnNames = new List<string>(FilterForIgnoredColumns(definingTableColumns.Select(c => GetBaseObjectIdentifier(c.Name)))),
						ReferencedTableName = foreignTable.ToString(),
						ReferencedTableColumnNames = new List<string>(FilterForIgnoredColumns(foreignColumns.Select(c => GetBaseObjectIdentifier(c.Name)))),
					});
				}
				//	Let the referenced table 'know' it is refererenced by the defining table
				if (tables.ContainsTable(foreignTable.ToString())) {
					Table toTable = tables[foreignTable.ToString()];
					toTable.InnerKeys.Add(new Key() {
						Name = foreignKey.Name.ToString(),
						ReferencedTableName = toTable.SchemaQualifiedName,
						ReferencedTableColumnNames = new List<string>(FilterForIgnoredColumns(foreignColumns.Select(c => GetBaseObjectIdentifier(c.Name)))),
						ReferencingTableName = definingTable.ToString(),
						ReferencingTableColumnNames = new List<string>(FilterForIgnoredColumns(definingTableColumns.Select(c => GetBaseObjectIdentifier(c.Name)))),
					});
				}
			}
		}

		private IEnumerable<string> FilterForIgnoredColumns(IEnumerable<string> enumerable) {
			return enumerable.Where(c => !IsIgnoredColumn(string.Empty, c));
		}

		private Table CreateTable(TSqlObject obj, string schemaName) {
			Table table = new Table();
			table.Name = GetBaseObjectName(obj.Name);
			table.Schema = schemaName;
			table.CleanName = CleanForUseInGeneratedCode(obj.Name, SingularizeTableNames);
			table.Columns = new List<Column>();
			table.ClassName = CleanForUseInGeneratedCode(obj.Name, true);
			table.LowerCamelClassName = ObjectNameHandler.LowerCamel(table.ClassName);
			foreach (TSqlObject child in obj.GetChildren()) {
				if (child.ObjectType == SqlColumn.TypeClass) {
					(string tableName, string column) = GetTableColumn(child);
					if (!IsIgnoredColumn(tableName, column)) {
						var col = GenerateColumn(child);
						table.Columns.Add(col);
					}
				}
				else if (child.ObjectType == PrimaryKeyConstraint.TypeClass) {
					foreach (var relation in child.GetReferencedRelationshipInstances()) {
						if (relation.Relationship.FromObjectClass == PrimaryKeyConstraint.TypeClass
							&& relation.Object.ObjectType == SqlColumn.TypeClass) {
							(string tableName, string column) = GetTableColumn(relation.Object);
							if (!IsIgnoredColumn(tableName, column)) {
								var colName = CleanForUseInGeneratedCode(relation.Object.Name, false);
								table[colName].IsPK = true;
							}
							//	TODO: LOG A WARNING HERE INDICATING A PK COLUMN HAS BEEN "IGNORED" IN GENERATION
						}
					}
				}
			}

			return table;
		}

		private Column GenerateColumn(TSqlObject child) {
			(string table, string column) = GetTableColumn(child);
			var tableColumnName = $"[{child.Name.Parts[0]}].[{child.Name.Parts[1]}].[{child.Name.Parts[2]}]";

            var isComputed = child.GetMetadata<ColumnType>(SqlColumn.ColumnType) == ColumnType.ComputedColumn;

			var dataTypes 
				= isComputed
				? child.GetReferencedRelationshipInstances(SqlColumn.ExpressionDependencies, DacExternalQueryScopes.UserDefined)
				: child.GetReferencedRelationshipInstances(SqlColumn.DataType);
						
			var sqlDataType = dataTypes.FirstOrDefault()?.ObjectName.ToString();
			bool hasOverrideType = false;
			if (sqlDataType is null) {
				//	Computed columns are not reported with column data types.
				//	the reader can be setup with a column type mapping.
				sqlDataType = TryGetColumnTypeMapping(table, column);
				if (!string.IsNullOrWhiteSpace(sqlDataType)) {
					hasOverrideType = true;
				}
			}
			var propertyName = CleanForUseInGeneratedCode(child.Name, false);
			var propertyType = GetPropertyType(sqlDataType);
			var length = child.GetProperty<int>(SqlColumn.Length);
			var precision = child.GetProperty<int>(SqlColumn.Precision);
			var scale = child.GetProperty<int>(SqlColumn.Scale);
			var comment = ColumnDescription.FirstOrDefault(d => d.TableName == table && d.ColumnName == column);

			sqlDataType = DecorateDataType(sqlDataType, precision, scale, length);

			EnumMapping? enumMapping = _EnumMappings.FirstOrDefault(c => string.Equals(c.TableColumnName, tableColumnName, StringComparison.OrdinalIgnoreCase));

			return new Column() {
				Ignore = false,
				IsPK = false,
				IsComputed = isComputed,
				IsAutoIncrement = child.GetProperty<bool>(SqlColumn.IsIdentity),
				IsNullable = child.GetProperty<bool>(SqlColumn.Nullable),
				MaximumLength = length,
				Name = propertyName,
				NumericPrecision = precision,
				NumericScale = scale,
				SqlDataType = sqlDataType,
				PropertyType = propertyType,
				EnumMappingPropertyType = enumMapping?.EnumName,
				PropertyName = propertyName,
				RequiresNullableIndicator = RequiresNullableIndicator(propertyType),
				HasOverrideType = hasOverrideType,
				Comment = comment?.Description,
			};
		}


		/// <summary>
		/// Take a multipart object identifier, extract the object name
		/// and cleanup the identifier for c#
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="singularize"></param>
		/// <returns></returns>
		protected string CleanForUseInGeneratedCode(ObjectIdentifier identifier, bool singularize) {
			string name = GetBaseObjectIdentifier(identifier);
			name = GetStrippedTableName(name);
			return ObjectNameHandler.CleanForUseInGeneratedCode(name, singularize);
		}

		/// <summary>
		/// Return the object name from an object identifier (which may include [database].[schema])
		/// maintains the '[]' identifier marks
		/// </summary>
		/// <param name="identifier"></param>
		/// <returns></returns>
		protected static string GetBaseObjectIdentifier(ObjectIdentifier identifier) {

			return identifier.Parts.Last();
		}

		protected static string GetBaseObjectName(ObjectIdentifier identifier) {
			return identifier.Parts.Last().Replace("[", "").Replace("]", "");
		}

		protected (string table, string column) GetTableColumn(TSqlObject child) {
			if (child.ObjectType != SqlColumn.TypeClass) {
				return (string.Empty, string.Empty);
			}
			string column = child.Name.Parts.Last();
			string table = child.Name.Parts.Reverse().Skip(1).First();
			return (table, column);
		}


	}
}
