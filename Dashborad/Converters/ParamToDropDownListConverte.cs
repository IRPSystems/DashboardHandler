using System.Globalization;
using System.Windows.Data;
using Entities.Models;

namespace Dashboard.Converters
{
	public class ParamToDropDownListConverte : IValueConverter
	{

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is IParamWithDropDown withDropDown))
				return null;

			return withDropDown.DropDown;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}



	}
}
