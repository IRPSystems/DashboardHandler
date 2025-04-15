
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Windows;

namespace DashboardHandler.ViewModels
{
	public class StencilViewModel: ObservableObject
	{
		#region Properties

		//public Stencil Stencil { get; set; }

		public SymbolCollection SymbolSource { get; set; }

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

		#endregion Mesthods
	}
}
