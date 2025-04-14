using Controls.ViewModels;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace DashboardHandler.ViewModels
{
	public class DocingViewModel : DocingBaseViewModel
	{
		#region Fields

		private ContentControl _udsServicesRequestsSection;

		#endregion Fields

		#region Constructor

		public DocingViewModel() :
			base("DockingMain", "DashboardHandler")
		{

			CreateWindows();
		}

		#endregion Constructor

		#region Methods

		private void CreateWindows()
		{
			DockFill = true;
		}

		#endregion Methods
	}
}
