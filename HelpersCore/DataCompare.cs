using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers.Core {
	public static class DataCompare {

		static public bool CheckEqual(string LHS, string RHS) {
			return string.Equals(LHS, RHS, StringComparison.CurrentCultureIgnoreCase);
		}

		public static bool CheckEqual<T>(IEnumerable<T> rhs, IEnumerable<T> lhs) where T : IEquatable<T> {
			if (rhs == null && lhs == null) {
				return true;
			}

			if (rhs != lhs && (rhs == null || lhs == null)) {
				return false;
			}

			if (rhs.Count() != lhs.Count()) {
				return false;
			}

			foreach (T r in rhs) {
				if (!lhs.Any(l => l.Equals(r))) {
					return false;
				}
			}
			return true;
		}

		static public bool CheckEqual(object LHS, object RHS) {
			return LHS?.Equals(RHS) ?? RHS == null;
		}

		static public bool CheckEqual<T>(T LHS, T RHS) where T : IEquatable<T> {
			return LHS?.Equals(RHS) ?? RHS == null;
		}

		static public bool CheckEqual<T>(Nullable<T> LHS, Nullable<T> RHS) where T : struct, IEquatable<T> {
			if (LHS.HasValue && RHS.HasValue) {
				return CheckEqual<T>((T)LHS.Value, (T)RHS.Value);
			}

			return !LHS.HasValue && !RHS.HasValue;
		}

		static public bool CheckEqual(byte[] LHS, byte[] RHS) {
			if (null != LHS && null != RHS) {
				return LHS.SequenceEqual(RHS);
			}
			else {
				return LHS == RHS;
			}
		}


	}

}
