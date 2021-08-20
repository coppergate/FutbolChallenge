using System;

namespace Attributes.Core {
	public class DataColumnAttribute : Attribute {
		public string DataType { get; set; }
		public bool AllowNulls { get; set; }
		public int MaxLength { get; set; }
		public int NumericPrecision { get; set; }
		public int NumericScale { get; set; }
		public string UnderlyingColumn { get; set; }
		public bool AutoIncrement { get; set; }
	}
}

