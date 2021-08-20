using System;

namespace Attributes.Core {
	public class DataColumnCommentAttribute : Attribute {
		public DataColumnCommentAttribute(string comment) {
			Comment = comment;
		}

		public string Comment { get; set; }
	}
}

