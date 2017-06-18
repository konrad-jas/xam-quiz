using System;
using System.Globalization;
using Xamarin.Forms;

namespace QuizApp.Core.Converters
{
	public class AnalysisEnabledTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool) value ? "Disable emotion analysis" : "Enable emotion analysis";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
