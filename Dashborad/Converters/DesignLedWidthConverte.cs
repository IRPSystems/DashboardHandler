using System.Globalization;
using System.Windows.Data;
using Entities.Models;

namespace Dashboard.Converters
{
	public class DesignLedWidthConverte : IValueConverter
	{

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double height))
				return null;

			return height - 5;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}



	}
}
