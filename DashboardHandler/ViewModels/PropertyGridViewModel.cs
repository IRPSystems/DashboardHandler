
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardHandler.Models.ToolsDesign;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.Windows.PropertyGrid;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
    public class PropertyGridViewModel: ObservableObject
    {
		#region Properties

		public DesignToolBase SelectedNode { get; set; }

		public Brush Background { get; set; }
		public Brush Foreround { get; set; }

		#endregion Properties

		#region Constructor

		public PropertyGridViewModel() 
		{
			Grid_AutoGeneratingPropertyGridItemCommand = 
				new RelayCommand<AutoGeneratingPropertyGridItemEventArgs>(Grid_AutoGeneratingPropertyGridItem);
		}

		#endregion Constructor

		#region Methods

		private void Grid_AutoGeneratingPropertyGridItem(
			AutoGeneratingPropertyGridItemEventArgs e)
		{

		}

		public void ChangeDarkLight()
		{
			Background =
				App.Current.Resources["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
			Foreround =
				App.Current.Resources["MahApps.Brushes.ThemeForeground"] as SolidColorBrush;
		}

		#endregion Methods

		#region Commands

		public RelayCommand<AutoGeneratingPropertyGridItemEventArgs> Grid_AutoGeneratingPropertyGridItemCommand { get; private set; }

		#endregion Commands
	}
}
