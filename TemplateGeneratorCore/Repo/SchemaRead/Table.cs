using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateCodeGenerator.SchemaRead {
	public class Table {
		public List<Column> Columns;
		public List<Key> InnerKeys = new List<Key>();
		public List<Key> OuterKeys = new List<Key>();

		public IEnumerable<Column> ColumnsWithoutKeys => Columns.Where(c => !c.IsAutoIncrement && !OuterKeys.Any(k => k.ReferencingTableColumnNames.Any(r => string.Equals(c.Name, r))));

		public string Name;
		public string Schema;
		public bool IsView;
		public bool MakeReadOnly = false;

		//	Should contain the table name with only alpha/numeric/underscore...
		public string CleanName;
		//	Should contain a valid C# identifier
		public string ClassName;
		public string LowerCamelClassName;

		public string SchemaQualifiedName => $"[{Schema}].[{Name}]";
		public bool Ignore = false;

		public string PkParameter { get; private set; }
		public string PkString { get; private set; }
		public string UpdateParameter { get; private set; }
		public string DeleteParameter { get; private set; }
		public string ValueParameter { get; private set; }

		protected string AllCols => string.Join(",", Columns.Where(k => !k.Ignore).Select(c => $"[{c.Name}]"));
		protected string InsertCols => string.Join(", ", Columns.Where(k => !k.IsAutoIncrement && !k.Ignore && !k.IsComputed).Select(c => $"[{c.Name}]"));
		protected string PkFilter => string.Join(" AND ", Columns.Where(c => c.IsPK && !c.Ignore).Select(c => $"[{c.Name}] = @{c.Name}"));

		public string SelectSqlWithPK { get; private set; }
		public string SelectSql { get; private set; }
		public string InsertSql { get; private set; }
		public string MergeSql { get; private set; }
		public string MergeWithKeepSql { get; private set; }

		public string UpdateSql { get; private set; }
		public string DeleteSql { get; private set; }

		public string QuotedSelectSqlWithPK => $"\"{SelectSqlWithPK}\"";
		public string QuotedSelectSql => $"\"{SelectSql}\"";
		public string QuotedInsertSql => $"\"{InsertSql}\"";
		public string QuotedMergeSql => $"\"{MergeSql}\"";
		public string QuotedMergeWithKeepSql => $"\"{MergeWithKeepSql}\"";
		public string QuotedUpdateSql => $"\"{UpdateSql}\"";
		public string QuotedDeleteSql => $"\"{DeleteSql}\"";

		public void BuildSql() {

			UpdateParameter = string.Join(", ", Columns.Where(k => !k.IsAutoIncrement && !k.Ignore && !k.IsComputed && !k.IsComputed).Select(c => $"[{c.Name}] = @{c.Name}"));
			ValueParameter = string.Join(", ", Columns.Where(k => !k.Ignore && !k.IsComputed).Select(c => $"@{c.Name} = value.{c.Name}"));
			DeleteParameter = string.Join(", ", Columns.Where(c => c.IsPK && !c.Ignore).Select(c => $"{c.Name} = value.{c.Name}"));

			GenerateSelect();
			GenerateInsert();
			GenerateMerge();
			GenerateUpdate();
			GenerateDelete();

			PkParameter = string.Join(" , ", Columns.Where(c => c.IsPK).Select(c => $"@{c.Name} = {c.Name}"));
		}

		private void GenerateDelete() {
			DeleteSql = $"DELETE {SchemaQualifiedName} WHERE {PkFilter};";
		}

		private void GenerateUpdate() {
			UpdateSql = $"UPDATE {SchemaQualifiedName} SET {UpdateParameter} WHERE {PkFilter} ";
		}

		private void GenerateMerge() {

			string mergeCTE = string.Join(", ", Columns.Where(k => !k.Ignore && !k.IsComputed).Select(
																	c => {
																		return c.PropertyType == "xml" ? $"[{c.Name}] = CONVERT(xml, @{c.Name})"
																					 : $"[{c.Name}] = @{c.Name}";
																	}));

			string mergeOn = string.Join(" AND ", Columns.Where(c => c.IsPK && !c.Ignore && !c.IsComputed).Select(c => $"T.[{c.Name}] = input.[{c.Name}]"));
			string mergeOutput = string.Join(", ", Columns.Where(c => c.IsPK && !c.Ignore && !c.IsComputed).Select(c => $"INSERTED.[{c.Name}]"));
			string mergeUpdate = string.Join(", ", Columns.Where(k => !k.IsAutoIncrement && !k.IsPK && !k.Ignore && !k.IsComputed).Select(c => $"[{c.Name}] = input.[{c.Name}]"));
			string mergeWithKeepUpdate = string.Join(", ", Columns.Where(k => !k.IsAutoIncrement && !k.IsPK && !k.Ignore && !k.IsComputed).Select(c => $"[{c.Name}] = ISNULL(input.[{c.Name}], T.[{c.Name}] )"));
			string mergeInsert = string.Join(", ", Columns.Where(k => !k.IsAutoIncrement && !k.Ignore && !k.IsComputed).Select(c => $"input.[{c.Name}]"));
			string mergeOutputVariablesDeclaration = string.Join(", ", Columns.Where(c => c.IsPK && !c.Ignore && !c.IsComputed).Select(c => $"{c.Name} {c.PropertyType}"));
			string mergeOutputVariables = string.Join(", ", Columns.Where(c => c.IsPK && !c.Ignore && !c.IsComputed).Select(c => $"@{c.Name}"));

			StringBuilder bldr = new StringBuilder();

			bldr.Clear();
			bldr.Append($" WITH input as (SELECT {mergeCTE}) MERGE {SchemaQualifiedName} AS T USING input ON {mergeOn} ");
			bldr.Append($" WHEN MATCHED THEN UPDATE SET {mergeUpdate} WHEN NOT MATCHED THEN INSERT ({InsertCols}) VALUES({mergeInsert}) ;");
			if (Columns.Any(c => c.IsAutoIncrement)) {
				bldr.Append($" SELECT SCOPE_IDENTITY();");
			}
			MergeSql = bldr.ToString();

			bldr.Clear();
			bldr.Append($" WITH input as (SELECT {mergeCTE}) MERGE {SchemaQualifiedName} AS T USING input ON {mergeOn} ");
			bldr.Append($" WHEN MATCHED THEN UPDATE SET {mergeWithKeepUpdate} WHEN NOT MATCHED THEN INSERT ({InsertCols}) VALUES({mergeInsert}) ;");
			if (Columns.Any(c => c.IsAutoIncrement)) {
				bldr.Append($" SELECT SCOPE_IDENTITY();");
			}
			MergeWithKeepSql = bldr.ToString();
		}

		private void GenerateInsert() {
			string insertParamCols = $"@{string.Join(", @", Columns.Where(k => !k.IsAutoIncrement && !k.Ignore && !k.IsComputed).Select(c => c.Name))}";
			StringBuilder bldr = new StringBuilder();
			bldr.Append($"INSERT {SchemaQualifiedName} ({InsertCols}) VALUES ({insertParamCols}); ");
			if (AutoIncrementPK) {
				bldr.Append(" SELECT SCOPE_IDENTITY(); ");
			}
			InsertSql = bldr.ToString();
		}

		private void GenerateSelect() {
			SelectSql = $"SELECT {AllCols} FROM {SchemaQualifiedName} ";

			if (!string.IsNullOrWhiteSpace(PkFilter)) {
				SelectSqlWithPK = $"SELECT { AllCols} FROM {SchemaQualifiedName} WHERE {PkFilter};";
			}
			else {
				SelectSqlWithPK = SelectSql;
			}
		}

		public bool AutoIncrementPK => this.Columns.Any(c => c.IsAutoIncrement && c.IsPK);

		public string AutoIncrementingPKType => this.Columns.Where(c => c.IsAutoIncrement && c.IsPK).Select(k => k.PropertyType).FirstOrDefault() ?? "void ";

		public Column GetColumn(string columnName) {
			return Columns.SingleOrDefault(x => string.Compare(x.Name, columnName, true) == 0);
		}

		public Column this[string columnName] => GetColumn(columnName);

	}




}