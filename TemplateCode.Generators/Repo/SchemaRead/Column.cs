using System;

namespace TemplateCodeGenerator.SchemaRead {
	public class Column {
		public string Name;
		public string Comment;
		public string PropertyName;
		public string PropertyType;
		public string SqlDataType;

		public string EnumMappingPropertyType;
		public bool HasEnumMapping => !string.IsNullOrEmpty(EnumMappingPropertyType);

		public int MaximumLength;
		public int NumericPrecision;
		public int NumericScale;
		public bool IsPK;
		public bool IsNullable;
		public string AllowNulls => IsNullable ? "true" : "false";

		public bool IsAutoIncrement;
		public bool IsComputed;
		public bool Ignore = false;

		public bool RequiresNullableIndicator;
		public bool RequiresDTONullableIndicator => !IsPK && RequiresNullableIndicator;

		public bool IsCalculated = false;
		public bool HasOverrideType = false;


		public string UpdatePropertyState = $"CurrentState = ObjectRecordState.{ObjectRecordState.MODIFIED};";


		public string ToCodeProperty(bool withEnum = false) {
			string nullableIndicator = IsNullable && RequiresNullableIndicator ? "?" : "";
			string property = "{ get; set; }";
			string type = (withEnum && HasEnumMapping) ? EnumMappingPropertyType : PropertyType;
			return $"public {type}{nullableIndicator} {PropertyName} {property}";
		}

		public string ToCodePropertyWithBackingField(int tabIndent, bool withEnum = false) {
			string propType = ToPropertyType(withEnum);
            string indent = new string('\t', tabIndent);
			string lengthAttrib
				= propType.StartsWith("string") && MaximumLength > 0
				? $"{indent}[StringLength({MaximumLength})]{Environment.NewLine}"
				: string.Empty;
			string field = $"private {propType} {PropertyName}Field;";
			string property = $@"public {propType} {PropertyName} {{ get {{ return {PropertyName}Field; }} set {{ {PropertyName}Field = value; {UpdatePropertyState}}} }}";
			return $"{indent}{field}{Environment.NewLine}{lengthAttrib}{indent}{property}{Environment.NewLine}";
		}

		public string ToPropertyType(bool withEnum = false) {
			string nullableIndicator = IsNullable && RequiresNullableIndicator ? "?" : "";
			string type = (withEnum && HasEnumMapping) ? EnumMappingPropertyType : PropertyType;
			return $"{type}{nullableIndicator} ";
		}

		public string ToCopyToDomain(string source, bool withEnum = true) {
			string copyValue;
			if (HasEnumMapping) {
				copyValue = $"{source}.{PropertyName}.ToEnumByEnumMemberAttribute<{EnumMappingPropertyType}>()";
			}
			else {
				if (RequiresDTONullableIndicator && !IsNullable) {
					copyValue = $"{source}.{PropertyName}.Value";
				}
				else {
					copyValue = $"{source}.{PropertyName}";
				}
			}
			return copyValue;
		}
		public string ToCopyToDTO(string source, bool withEnum = true) {
			string copyValue;
			if (HasEnumMapping) {
				copyValue = $"{source}.{PropertyName}.ToEnumString()";
			}
			else {
				copyValue = $"{source}.{PropertyName}";
			}
			return copyValue;
		}
	}

}

