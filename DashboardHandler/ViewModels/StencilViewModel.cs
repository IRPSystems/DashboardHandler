
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Windows;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
	public class StencilViewModel: ObservableObject
	{
		#region Properties

		//public Stencil Stencil { get; set; }

		public SymbolCollection SymbolSource { get; set; }

		public Brush Background { get; set; }
		public Brush Foreround { get; set; }

		#endregion Properties

		#region Constructor

		public StencilViewModel()
		{
			SymbolSource = new SymbolCollection();


			AddSymbols();
		}

		#endregion Constructor

		#region Mesthods

		private void AddSymbols()
		{
			SymbolViewModel symbol = new SymbolViewModel()
			{
				Symbol = "Switch",
				Name = "Switch",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "ComboBox",
				Name = "ComboBox",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "TextBox",
				Name = "TextBox",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);


			symbol = new SymbolViewModel()
			{
				Symbol = "Led",
				Name = "Led",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Gauge",
				Name = "Gauge",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Chart",
				Name = "Chart",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Register",
				Name = "Register",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "MonitorList",
				Name = "MonitorList",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "CommandsList",
				Name = "CommandsList",
				SymbolTemplate = App.Current.Resources["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);
		}

		public void ChangeDarkLight()
		{
			Background =
				App.Current.Resources["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
			Foreround =
				App.Current.Resources["MahApps.Brushes.ThemeForeground"] as SolidColorBrush;
		}

		#endregion Mesthods
	}
}
