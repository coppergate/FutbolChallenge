using System;
using System.Text.RegularExpressions;

namespace TemplateCodeGenerator.SchemaRead {


	static public class ObjectNameHandler {
		//	Basic regex to strip non-alphanumeric characters
		static readonly Regex rxCleanUp = new Regex(@"[^\w\d]", RegexOptions.Compiled);

		/// <summary>
		/// clean a string for use as a c# property name
		/// replace non-alphanumeric chars with '_'
		/// add leading '_' to strings which start with a number
		/// optionally singularize the string.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="singularize"></param>
		/// <returns></returns>
		public static string CleanForUseInGeneratedCode(string str, bool singularize) {
			str = rxCleanUp.Replace(str, "_");
			if (char.IsDigit(str[0])) {
				str = "_" + str;
			}

			if (singularize) {
				str = Singularize(str);
			}
			return str.Replace("_", "");
		}

		public static string Singularize(string str) {
			if (str.EndsWith("s", StringComparison.CurrentCultureIgnoreCase)
				&& !str.EndsWith("ss", StringComparison.CurrentCultureIgnoreCase)
				&& !str.EndsWith("ies", StringComparison.CurrentCultureIgnoreCase)) {
				str = str.Substring(0, str.Length - 1);
			}
			return str;
		}

		public static string LowerCamel(string str) => char.ToLowerInvariant(str[0]) + str.Substring(1);
	}
}
