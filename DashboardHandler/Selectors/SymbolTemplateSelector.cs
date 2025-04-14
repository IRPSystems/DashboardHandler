using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;


namespace DashboardHandler.Selectors
{
	public class SymbolTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;

			if (!(item is SymbolViewModel symbol))
				return null;

			if (symbol.Name == "Switch")
				return element.FindResource("SwitchTemplate") as DataTemplate;
			else if (symbol.Name == "ComboBox")
				return element.FindResource("ComboBoxTemplate") as DataTemplate;
			else if (symbol.Name == "TextBox")
				return element.FindResource("TextBoxTemplate") as DataTemplate;
			else if (symbol.Name == "Led")
				return element.FindResource("LedTemplate") as DataTemplate;
			else if (symbol.Name == "Gauge")
				return element.FindResource("GaugeTemplate") as DataTemplate;
			else if (symbol.Name == "Chart")
				return element.FindResource("ChartTemplate") as DataTemplate;
			else if (symbol.Name == "Register")
				return element.FindResource("RegisterTemplate") as DataTemplate;


			return null;
		}
	}
}
