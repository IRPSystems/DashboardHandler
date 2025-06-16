
using System.Windows.Data;
using Dashboard.Models.ToolsDesign;

namespace Dashboard.Converters
{

	public class SwitchOnOffDescriptionConverte : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (!(values[1] is DesignToolSwitch tool))
				return null;

			if (tool.IsChecked)
				return tool.OnDescription;
			else
				return tool.OffDescription;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
