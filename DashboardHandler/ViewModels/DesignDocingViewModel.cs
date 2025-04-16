using Controls.ViewModels;
using DashboardHandler.Views;
using DeviceHandler.Models;
using DeviceHandler.ViewModel;
using DeviceHandler.Views;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Concurrent;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

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

		#endregion Fields

		#region Constructor

		public DesignDocingViewModel(
			DevicesContainer devicesContainer) :
			base("DesignDocking", "DashboardHandler")
		{
			CreateWindows(devicesContainer);

			NameToDesignDashboardDict = new ConcurrentDictionary<string,DesignDashboardViewModel>();
		}

		#endregion Constructor

		#region Methods

		private void CreateWindows(DevicesContainer devicesContainer)
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

			StencilView stencilView = new StencilView();
			CreateWindow(
				stencilView,
				"Stencil",
				"Stencil",
				DockSide.Left,
				out _stencil);
		}

		public bool AddDashboard(DesignDashboardViewModel vm)
		{
			if (NameToDesignDashboardDict.ContainsKey(vm.Name))
			{
				return false;
			}

			NameToDesignDashboardDict[vm.Name] = vm;

			DesignDashboardView dashboardView = new DesignDashboardView()
			{ DataContext = vm };

			ContentControl window;
			CreateWindow(
				dashboardView,
				string.Empty,
				vm.Name,
				DockSide.None,
				out window);


			SetState(window, DockState.Document);

			return true;
		}

		#endregion Methods
	}
}
