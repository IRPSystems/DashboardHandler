using Controls.ViewModels;
using DashboardHandler.Views;
using DeviceHandler.Models;
using DeviceHandler.ViewModel;
using DeviceHandler.Views;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DashboardHandler.ViewModels
{
	public class DesignDokcingViewModel : DocingBaseViewModel
	{
		#region Properties

		public Dictionary<string,DesignDashboardViewModel> NameToDesignDashboardDict { get; set; }

		#endregion Properties

		#region Fields

		private ContentControl _parametrs;
		private ContentControl _stencil;
		private ContentControl _propertyGrid;

		#endregion Fields

		#region Constructor

		public DesignDokcingViewModel(
		DevicesContainer devicesContainer,
			PropertyGridViewModel propertyGrid,
			StencilViewModel stencil) :
			base("DesignDokcking", "DashboardHandler")
		{
			CreateWindows(devicesContainer, propertyGrid, stencil);

			CloseAllTabs += DesignDocingViewModel_CloseAllTabs;
			CloseButtonClick += DesignDocingViewModel_CloseButtonClick;
			CloseOtherTabs += DesignDocingViewModel_CloseOtherTabs;

			NameToDesignDashboardDict = new Dictionary<string,DesignDashboardViewModel>();
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
			SetCanClose(_parametrs, false);
			SetDesiredWidthInDockedMode(_parametrs, 300);

			PropertyGridView propertiesGridView = new PropertyGridView()
			{ DataContext = propertyGrid };
			CreateWindow(
				propertiesGridView,
				"Properties",
				"Properties",
				DockSide.Bottom,
				out _propertyGrid);
			SetTargetName(_propertyGrid, "Parameters", DockState.Dock);
			SetCanClose(_propertyGrid, false);

			StencilView stencilView = new StencilView()
			{ DataContext = stencil };
			CreateWindow(
				stencilView,
				"Stencil",
				"Stencil",
				DockSide.Left,
				out _stencil);
			SetCanClose(_stencil, false);
			SetDesiredWidthInDockedMode(_stencil, 300);
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

		#region Close window

		private void DesignDocingViewModel_CloseOtherTabs(object sender, CloseTabEventArgs e)
		{
			List<ContentControl> childrensToRemove = new List<ContentControl>();
			foreach (ContentControl window in Children)
			{
				if (!(window.Content is DesignDashboardView dashboardV))
					continue;

				if (!(dashboardV.DataContext is DesignDashboardViewModel dashboardVM))
					continue;

				if (!(e.ClosingTabItems[0] is TabItemExt tabItemExt))
					continue;

				if (!(tabItemExt.Header is string tabHeader))
					continue;

				if (dashboardVM.DesignDiagram.Name == tabHeader)
				{
					childrensToRemove.Add(window);
				}
			}

			RemoveWindows(childrensToRemove);
		}

		private void DesignDocingViewModel_CloseButtonClick(object sender, CloseButtonEventArgs e)
		{
			List<ContentControl> childrensToRemove = new List<ContentControl>()
			{
				e.TargetItem as ContentControl,
			};

			RemoveWindows(childrensToRemove);
		}

		private void DesignDocingViewModel_CloseAllTabs(object sender, CloseTabEventArgs e)
		{
			List<ContentControl> childrensToRemove = new List<ContentControl>();
			foreach (ContentControl window in Children)
			{
				childrensToRemove.Add(window);
			}

			RemoveWindows(childrensToRemove);
		}

		private void RemoveWindows(List<ContentControl> childrensToRemove)
		{
			foreach (ContentControl window in childrensToRemove)
			{
				Children.Remove(window);

				if (!(window.Content is DesignDashboardView dashboardV))
					continue;

				if (!(dashboardV.DataContext is DesignDashboardViewModel dashboardVM))
					continue;

				var item = NameToDesignDashboardDict.Where(
					kvp => kvp.Value.Equals(dashboardVM)).ToList();
				if (item == null || item.Count() == 0)
					continue;


				NameToDesignDashboardDict.Remove(
					item[0].Key);

			}
		}

		#endregion Close window

		#endregion Methods
	}
}
