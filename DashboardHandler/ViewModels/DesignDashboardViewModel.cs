
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.UI.Xaml.Diagram;

namespace DashboardHandler.ViewModels
{
	public class DesignDashboardViewModel: ObservableObject
	{
		#region Properties

		public SfDiagram Diagram { get; set; }
		public string Name { get; set; }

		#endregion Properties

		#region Constructor

		public DesignDashboardViewModel(string name)
		{
			Name = name;

			Diagram = new SfDiagram();
		}

		#endregion Constructor
	}
}
