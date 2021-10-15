using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace FutbolChallengeUI.Converters
{
	public class ShowControlBoolToVisibilityEnumConverter : IValueConverter
	{

		public object Convert(object value, Type targetType,
								object parameter, string language)
		{
			if (value == null || (bool)value)
			{
				return Visibility.Visible;
			}
			else
			{
				return Visibility.Collapsed;
			}

			throw new InvalidCastException($"Failed converting {value} to Visibility enum");
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, string language)
		{
			Visibility visibility = (Visibility)value;

			if (visibility == Visibility.Visible)
			{
				return true;
			}
			else
			{
				return false;
			}

			throw new InvalidCastException($"Failed converting {value} to ShowControl bool");
		}

	}
}
