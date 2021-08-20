using System.Collections.Generic;
using System.Linq;

namespace TemplateCodeGenerator.SchemaRead {
	public class Tables {
		readonly Dictionary<string, Table> _Tables = new Dictionary<string, Table>();

		public Tables() {
		}

		public IEnumerable<Table> All => _Tables.Select(e => e.Value);

		public void Add(Table table) {
			if (!_Tables.ContainsKey(table.SchemaQualifiedName)) {
				_Tables.Add(table.SchemaQualifiedName, table);
			}
		}

		public Table GetTable(string tableName) {
			if (_Tables.ContainsKey(tableName)) {
				return _Tables[tableName];
			}
			return null;
		}

		public bool ContainsTable(string table) {
			return _Tables.ContainsKey(table);
		}

		public Table this[string schemaQualifiedName] => GetTable(schemaQualifiedName);

		internal void RemoveAll(IEnumerable<string> tablesToRemove) {
			foreach (string tableName in tablesToRemove) {
				_Tables.Remove(tableName);
			}
		}
	}
}
