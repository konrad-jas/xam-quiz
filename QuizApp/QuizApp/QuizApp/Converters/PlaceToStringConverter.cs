using System;
using System.Globalization;
using Xamarin.Forms;

namespace QuizApp.Core.Converters
{
	public class PlaceToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var place = (int)value;

			switch (place)
			{
				case 1:
					return "1st";
				case 2:
					return "2nd";
				case 3:
					return "3rd";
				case 4:
					return "4th";
				case 5:
					return "5th";
			}

			return place.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
