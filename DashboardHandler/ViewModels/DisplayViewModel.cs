

using CommunityToolkit.Mvvm.ComponentModel;

namespace DashboardHandler.ViewModels
{
	public class DisplayViewModel: ObservableObject
	{
		public DisplayDashboardViewModel DisplayDashboard { get; set; }

		public DisplayViewModel()
		{
			DisplayDashboard = new DisplayDashboardViewModel();
		}
	}
}
