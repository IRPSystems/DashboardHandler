using Controls.ViewModels;
using DashboardHandler.Interfaces;
using DashboardHandler.Views;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Controls;

namespace DashboardHandler.ViewModels
{
	public class DocumantsDokcingViewModel : DocingBaseViewModel
	{
		#region Properties

		public Dictionary<string, IDashboardVM> NameToDashboardDict { get; set; }

		#endregion Properties


		#region Constructor

		public DocumantsDokcingViewModel(
			string layoutFileName,
			string dirName) :
			base(layoutFileName, dirName)
		{
			
			CloseAllTabs += DesignDocingViewModel_CloseAllTabs;
			CloseButtonClick += DesignDocingViewModel_CloseButtonClick;
			CloseOtherTabs += DesignDocingViewModel_CloseOtherTabs;
			WindowClosing += DocumantsDokcingViewModel_WindowClosing;

			NameToDashboardDict = new Dictionary<string, IDashboardVM>();
		}

		#endregion Constructor

		#region Methods

		public bool AddDashboard(
			IDashboardVM vm,
			UserControl dashboardView)
		{
			if(vm.DesignDiagram == null) 
				return false;

			if (NameToDashboardDict.ContainsKey(vm.DesignDiagram.Name))
			{
				return false;
			}

			NameToDashboardDict[vm.DesignDiagram.Name] = vm;
			dashboardView.DataContext = vm;

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

				if (!(dashboardV.DataContext is IDashboardVM dashboardVM))
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

		private void DocumantsDokcingViewModel_WindowClosing(object sender, WindowClosingEventArgs e)
		{
			List<ContentControl> childrensToRemove = new List<ContentControl>()
			{
				e.TargetItem as ContentControl,
			};

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

				if (!(window.Content is IDashboardV dashboardV))
					continue;

				if (!(dashboardV.DataContext is IDashboardVM dashboardVM))
					continue;

				var item = NameToDashboardDict.Where(
					kvp => kvp.Value.Equals(dashboardVM)).ToList();
				if (item == null || item.Count() == 0)
					continue;


				NameToDashboardDict.Remove(
					item[0].Key);

			}
		}

		#endregion Close window

		#endregion Methods
	}
}
