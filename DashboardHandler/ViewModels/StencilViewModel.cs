
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Windows;

namespace DashboardHandler.ViewModels
{
	public class StencilViewModel: ObservableObject
	{
		#region Properties

		public Stencil Stencil { get; set; }

		#endregion Properties

		#region Constructor

		public StencilViewModel()
		{
			Stencil = new Stencil();
			Stencil.SymbolSource = new SymbolCollection();
			Stencil.ShowSearchTextBox = false;

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
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "ComboBox",
				Name = "ComboBox",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "TextBox",
				Name = "TextBox",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);


			symbol = new SymbolViewModel()
			{
				Symbol = "Led",
				Name = "Led",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Gauge",
				Name = "Gauge",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Chart",
				Name = "Chart",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);

			symbol = new SymbolViewModel()
			{
				Symbol = "Register",
				Name = "Register",
			};
			(Stencil.SymbolSource as SymbolCollection).Add(symbol);
		}

		#endregion Mesthods
	}
}
