using Controls.ViewModels;
using DashboardHandler.Views;
using DeviceHandler.Models;
using DeviceHandler.ViewModel;
using DeviceHandler.Views;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Concurrent;
using System.Windows.Controls;

namespace DashboardHandler.ViewModels
{
	public class DesignDocingViewModel : DocingBaseViewModel
	{
		#region Properties

		public ConcurrentDictionary<string,DesignDashboardViewModel> NameToDesignDashboardDict { get; set; }

		#endregion Properties

		#region Fields

		private ContentControl _parametrs;
		private ContentControl _stencil;
		private ContentControl _propertyGrid;

		#endregion Fields

		#region Constructor

		public DesignDocingViewModel(
		DevicesContainer devicesContainer,
			PropertyGridViewModel propertyGrid,
			StencilViewModel stencil) :
			base("DesignDocking", "DashboardHandler")
		{
			CreateWindows(devicesContainer, propertyGrid, stencil);

			NameToDesignDashboardDict = new ConcurrentDictionary<string,DesignDashboardViewModel>();
		}

		#endregion Constructor

		#region Methods

		private void CreateWindows(
			DevicesContainer devicesContainer,
			PropertyGridViewModel propertyGrid,
			StencilViewModel stencil)
		{
			ParametersView parametersView = new ParametersView()
			{ 
				DataContext = new ParametersViewModel(
					new Entities.Models.DragDropData(), 
					devicesContainer, 
					false) 
			};
			CreateWindow(
				parametersView,
				"Parameters",
				"Parameters",
				DockSide.Right,
				out _parametrs);

			PropertyGridView propertiesGridView = new PropertyGridView()
			{ DataContext = propertyGrid };
			CreateWindow(
				propertiesGridView,
				"Properties",
				"Properties",
				DockSide.Bottom,
				out _propertyGrid);
			SetTargetName(_propertyGrid, "Parameters", DockState.Dock);

			StencilView stencilView = new StencilView()
			{ DataContext = stencil };
			CreateWindow(
				stencilView,
				"Stencil",
				"Stencil",
				DockSide.Left,
				out _stencil);
		}

		public bool AddDashboard(DesignDashboardViewModel vm)
		{
			if (NameToDesignDashboardDict.ContainsKey(vm.DesignDiagram.Name))
			{
				return false;
			}

			NameToDesignDashboardDict[vm.DesignDiagram.Name] = vm;

			DesignDashboardView dashboardView = new DesignDashboardView()
			{ DataContext = vm };

			ContentControl window;
			CreateWindow(
				dashboardView,
				string.Empty,
				vm.DesignDiagram.Name,
				DockSide.None,
				out window);


			SetState(window, DockState.Document);

			return true;
		}

		#endregion Methods
	}
}
