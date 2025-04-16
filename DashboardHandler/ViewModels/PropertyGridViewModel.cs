
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardHandler.Models.ToolsDesign;
using Syncfusion.Windows.PropertyGrid;

namespace DashboardHandler.ViewModels
{
    public class PropertyGridViewModel: ObservableObject
    {
		#region Properties

		public DesignToolBase SelectedNode { get; set; }

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

		#endregion Methods

		#region Commands

		public RelayCommand<AutoGeneratingPropertyGridItemEventArgs> Grid_AutoGeneratingPropertyGridItemCommand { get; private set; }

		#endregion Commands
	}
}
