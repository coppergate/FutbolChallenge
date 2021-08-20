using Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helpers {
	public static class DataGenerator {
		static readonly Random _Rand = new Random();

		static public void LoadObject<T>(T target) {
			foreach (PropertyInfo p in TypeHelpers.AllInstancePublicProperties<T>()) {
				ValidatableFieldAttribute dataSetup = p.GetCustomAttribute<ValidatableFieldAttribute>();
				if (p.SetMethod != null) {
					if (p.PropertyType == typeof(string)) {
						var attr = (TestValidateableStringAttribute)dataSetup;
						p.SetValue(target, GetString(attr.MinLength, attr.MaxLength));
					}
					else if (p.PropertyType == typeof(int)
						|| p.PropertyType == typeof(int?)) {
						var attr = (TestValidateableIntAttribute)dataSetup;
						p.SetValue(target, GetInt(attr.MinValue, attr.MaxValue));
					}
					else if (p.PropertyType == typeof(long)
						|| p.PropertyType == typeof(long?)) {
						var attr = (TestValidateableLongAttribute)dataSetup;
						p.SetValue(target, GetLong(attr.MinValue, attr.MaxValue));
					}
					else if (p.PropertyType == typeof(float)
						|| p.PropertyType == typeof(float?)) {
						var attr = (TestValidateableFloatAttribute)dataSetup;
						p.SetValue(target, GetFloat(attr.MinValue, attr.MaxValue));
					}
					else if (p.PropertyType == typeof(decimal)
						|| p.PropertyType == typeof(decimal?)) {
						var attr = (TestValidateableDecimalAttribute)dataSetup;
						p.SetValue(target, GetDecimal(attr.MinValueActual.Value, attr.MaxValueActual.Value));
					}
					else if (p.PropertyType == typeof(DateTime)
						|| p.PropertyType == typeof(DateTime?)) {
						p.SetValue(target, GetDate());
					}
					else if (p.PropertyType == typeof(byte)
						|| p.PropertyType == typeof(byte?)) {
						p.SetValue(target, GetByte());
					}
					else if (p.PropertyType.IsEnum) {
						p.SetValue(target, GetEnumValue(p.PropertyType));
					}
				}
			}
		}

		static public byte GetByte() {
			return Convert.ToByte(_Rand.Next(0, 2));
		}

		static public DateTime GetDate() {
			int offset = _Rand.Next(-600, 600);
			offset *= _Rand.Next(0, 2);
			return DateTime.Now.AddDays(offset);
		}

		static public decimal GetDecimal(decimal minValue = 0, decimal maxValue = decimal.MaxValue) {

			return (decimal)((decimal)_Rand.NextDouble() * (maxValue - minValue)) + minValue;
		}

		static public float GetFloat(float minValue = 0, float maxValue = float.MaxValue) {
			return (float)(_Rand.NextDouble() * (maxValue - minValue)) + minValue;
		}

		static public long GetLong(long minValue = 0, long maxValue = long.MaxValue) {
			long ret = (long)(_Rand.NextDouble() * (maxValue - minValue) + minValue);
			return ret;
		}

		static public int GetInt(int minValue = 0, int maxValue = int.MaxValue) {
			return _Rand.Next(maxValue - minValue) + minValue;
		}

		static public string GetString(int length) {
			return GetString(0, length);
		}

		static public string GetString(int minLength, int maxLength) {
			if (maxLength == 1) {
				return _Rand.Next(0, 2) == 0 ? "N" : "Y";
			}

			string source = "abcdefghijklmnopqrstuvwxyz";
			StringBuilder ret = new StringBuilder();
			int genLength = _Rand.Next(minLength, maxLength);
			for (int cnt = 0; cnt < genLength; ++cnt) {
				ret.Append(source[_Rand.Next(0, source.Length)]);
			}
			return ret.ToString();
		}

		public static object GetEnumValue(Type type) {
			if (!type.IsEnum) throw new ArgumentException("T must be an enumerated type");

			List<object> vals = new List<object>(Enum.GetValues(type).Cast<object>());

			return vals[GetInt(0, vals.Count - 1)];
		}

	}
}
