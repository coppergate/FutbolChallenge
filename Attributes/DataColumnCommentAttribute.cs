using System;

namespace Attributes {
	public class DataColumnCommentAttribute : Attribute {
		public DataColumnCommentAttribute(string comment) {
			Comment = comment;
		}

		public string Comment { get; set; }
	}
}

