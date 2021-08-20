using System;

namespace Attributes.Core {
	public class ValidatableFieldAttribute : Attribute {
		public string DataType { get; set; }
		public bool Nullable { get; set; } = true;
	}

	public class TestValidateableIntAttribute : ValidatableFieldAttribute {
		public TestValidateableIntAttribute() {
			DataType = typeof(int).FullName;
		}

		public int MinValue { get; set; } = 0;
		public int MaxValue { get; set; } = int.MaxValue;

	}

	public class TestValidateableLongAttribute : ValidatableFieldAttribute {
		public TestValidateableLongAttribute() {
			DataType = typeof(long).FullName;
		}

		public long MinValue { get; set; } = 0;
		public long MaxValue { get; set; } = long.MaxValue;
	}

	public class TestValidateableFloatAttribute : ValidatableFieldAttribute {
		public TestValidateableFloatAttribute() {
			DataType = typeof(float).FullName;
		}

		public float MinValue { get; set; } = 0;
		public float MaxValue { get; set; } = float.MaxValue;
	}

	public class TestValidateableDateTimeAttribute : ValidatableFieldAttribute {
		public TestValidateableDateTimeAttribute() {
			DataType = typeof(DateTime).FullName;
		}

		public DateTime MinValue { get; set; } = new DateTime(1900, 1, 1);
		public DateTime MaxValue { get; set; } = new DateTime(2999, 12, 31);
	}


	public class TestValidateableDecimalAttribute : ValidatableFieldAttribute {
		public TestValidateableDecimalAttribute() {
			DataType = typeof(decimal).FullName;
		}

		//	This is a kludge because 'decimal' can not be used as an attribute property type...
		public decimal? MinValueActual { get; set; } = 0;
		public decimal? MaxValueActual { get; set; } = decimal.MaxValue;

		public string MinValue {
			get { return MinValueActual.ToString(); }
			set { MinValueActual = Decimal.Parse(value); }
		}
		public string MaxValue {
			get { return MaxValueActual.ToString(); }
			set { MaxValueActual = Decimal.Parse(value); }
		}
	}


	public class TestValidateableStringAttribute : ValidatableFieldAttribute {
		public TestValidateableStringAttribute() {
			DataType = typeof(string).FullName;
		}

		public int MinLength { get; set; } = 0;
		public int MaxLength { get; set; } = 1024;

	}
}


