using Microsoft.UI.Xaml.Data;
using System;

namespace FutbolChallengeUI.Converters
{
	public class DateToStringConverter : IValueConverter
	{

		public object Convert(object value, Type targetType,
								object parameter, string language)
		{
			if(value == null)
			{
				return string.Empty;
			}

			DateTime thisdate = (DateTime)value;
			return thisdate.ToString("yyyy-MM-dd");
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, string language)
		{
			string dateString = (string)value;

			if (DateTime.TryParse(dateString, out DateTime dateVal ))
			{
				return dateVal;
			}
			return null;
		}

	}
}
