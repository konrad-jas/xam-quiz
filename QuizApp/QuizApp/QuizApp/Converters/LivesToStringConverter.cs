using System;
using System.Globalization;
using Xamarin.Forms;

namespace QuizApp.Core.Converters
{
	public class LivesToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var lives = (int) value;
			var dots = new char[lives];
			for (var i = 0; i < lives; ++i)
			{
				dots[i] = '\u26AB';
			}
			return new string(dots);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
