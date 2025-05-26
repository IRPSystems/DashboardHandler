using Controls.ViewModels;
using DashboardHandler.Views;
using DeviceHandler.Models;
using DeviceHandler.ViewModel;
using DeviceHandler.Views;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Controls;

namespace DashboardHandler.ViewModels
{
	public class DisplayDokcingViewModel : DocingBaseViewModel
	{
		#region Properties

		public Dictionary<string,DisplayDashboardViewModel> NameToDesignDashboardDict { get; set; }

		#endregion Properties


		#region Constructor

		public DisplayDokcingViewModel() :
			base("DisplayDokcking", "DashboardHandler")
		{
			
			CloseAllTabs += DesignDocingViewModel_CloseAllTabs;
			CloseButtonClick += DesignDocingViewModel_CloseButtonClick;
			CloseOtherTabs += DesignDocingViewModel_CloseOtherTabs;

			NameToDesignDashboardDict = new Dictionary<string,DisplayDashboardViewModel>();
		}

		#endregion Constructor

		#region Methods

		public bool AddDashboard(DisplayDashboardViewModel vm)
		{
			if(vm.DesignDiagram == null) 
				return false;

			if (NameToDesignDashboardDict.ContainsKey(vm.DesignDiagram.Name))
			{
				return false;
			}

			NameToDesignDashboardDict[vm.DesignDiagram.Name] = vm;

			DisplayDashboardView dashboardView = new DisplayDashboardView()
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

				if (!(dashboardV.DataContext is DisplayDashboardViewModel dashboardVM))
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

				if (!(dashboardV.DataContext is DisplayDashboardViewModel dashboardVM))
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
