using System.Collections.Generic;

namespace TemplateCodeGenerator.SchemaRead {
	public class Key {
		public string Name;
		public string ReferencedTableName;
		public List<string> ReferencedTableColumnNames = new List<string>();
		public string ReferencingTableName;
		public List<string> ReferencingTableColumnNames = new List<string>();
	}
}
