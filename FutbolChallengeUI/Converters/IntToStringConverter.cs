using Microsoft.UI.Xaml.Data;
using System;

namespace FutbolChallengeUI.Converters
{
	public class IntToStringConverter : IValueConverter
	{

		public object Convert(object value, Type targetType,
								object parameter, string language)
		{
			if (value == null)
				return string.Empty;

			if (!int.TryParse(value.ToString(), out int thisint))
				throw new InvalidCastException($"Failed converting integer {thisint} to string");

			return thisint.ToString();
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, string language)
		{

			string intString = (string)value;
			if (string.IsNullOrWhiteSpace(intString))
				throw new InvalidOperationException("Attempting to convert empty/null string to int");
		;

			if (!int.TryParse(intString, out int intVal))
				throw new InvalidCastException($"Failed converting {intString} to integer");

			return intVal;

		}

	}
}
