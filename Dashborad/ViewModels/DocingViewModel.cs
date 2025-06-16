using Controls.ViewModels;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace Dashboard.ViewModels
{
	public class DocingViewModel : DocingBaseViewModel
	{
		#region Fields


		#endregion Fields

		#region Constructor

		public DocingViewModel() :
			base("DockingMain", "Dashboard")
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
