
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Windows;
using System.Windows.Media;

namespace Dashboard.ViewModels
{
	public class StencilViewModel: ObservableObject
	{
		#region Properties

		//public Stencil Stencil { get; set; }

		public SymbolCollection SymbolSource { get; set; }

		public Brush Background { get; set; }
		public Brush Foreround { get; set; }

		#endregion Properties

		#region Fields

		private ResourceDictionary _appResoureces;

		#endregion Fields

		#region Constructor

		public StencilViewModel(ResourceDictionary appResoureces)
		{
			_appResoureces = appResoureces;

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
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "ComboBox",
				Name = "ComboBox",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "TextBox",
				Name = "TextBox",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);


			symbol = new SymbolViewModel()
			{
				Symbol = "Led",
				Name = "Led",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Gauge",
				Name = "Gauge",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Chart",
				Name = "Chart",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Register",
				Name = "Register",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "MonitorList",
				Name = "MonitorList",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "CommandsList",
				Name = "CommandsList",
				SymbolTemplate = _appResoureces["SymbolTemplate"] as DataTemplate,
			};
			SymbolSource.Add(symbol);
		}

		public void ChangeDarkLight()
		{
			Background =
				_appResoureces["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
			Foreround =
				_appResoureces["MahApps.Brushes.ThemeForeground"] as SolidColorBrush;
		}

		#endregion Mesthods
	}
}
