using System;

namespace Attributes.Core {

	public enum KeyDirection {
		IsReferencedBy,
		References
	}

	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ForeignKeyAttribute : Attribute {

		public string Key { get; set; }
		public KeyDirection Direction { get; set; }
		public string ToReference { get; set; }
		public string FromReference { get; set; }
		public string ToReferenceColumns { get; set; }
		public string FromReferenceColumns { get; set; }
	}
}

