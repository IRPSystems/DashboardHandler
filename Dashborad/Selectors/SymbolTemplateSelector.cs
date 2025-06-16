using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;


namespace Dashboard.Selectors
{
	public class SymbolTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;

			//if (!(item is SymbolViewModel symbol))
			//	return null;

			if (!(item is string name))
				return null;

			if (name =="Switch")
				return element.FindResource("SwitchTemplate") as DataTemplate;
			else if (name =="ComboBox")
				return element.FindResource("ComboBoxTemplate") as DataTemplate;
			else if (name =="TextBox")
				return element.FindResource("TextBoxTemplate") as DataTemplate;
			else if (name =="Led")
				return element.FindResource("LedTemplate") as DataTemplate;
			else if (name =="Gauge")
				return element.FindResource("GaugeTemplate") as DataTemplate;
			else if (name =="Chart")
				return element.FindResource("ChartTemplate") as DataTemplate;
			else if (name =="Register")
				return element.FindResource("RegisterTemplate") as DataTemplate;
			else if (name =="MonitorList")
				return element.FindResource("MonitorListTemplate") as DataTemplate;
			else if (name =="CommandsList")
				return element.FindResource("CommandsListTemplate") as DataTemplate;


			return null;
		}
	}
}
