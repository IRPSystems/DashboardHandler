
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardHandler.Models.ToolsDesign;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.Windows.PropertyGrid;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
    public class PropertyGridViewModel: ObservableObject
    {
		#region Properties

		public DesignToolBase SelectedNode { get; set; }

		public ObservableCollection<string> HidePropertiesList { get; set; }

		public Brush Background { get; set; }
		public Brush Foreround { get; set; }

		#endregion Properties

		#region Constructor

		public PropertyGridViewModel() 
		{
			HidePropertiesList = new ObservableCollection<string>();
		}

		#endregion Constructor

		#region Methods

		public void SetHideProperties(DesignToolBase tool)
		{ 
			HidePropertiesList.Clear();
			List<string> hideProperties = tool.GetHideProperties();
			foreach (string property in hideProperties)
				HidePropertiesList.Add(property);

			OnPropertyChanged(nameof(HidePropertiesList));
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


		#endregion Commands
	}
}
