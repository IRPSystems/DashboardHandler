
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeviceHandler.Models;
using System.Diagnostics.Metrics;

namespace DashboardHandler.ViewModels
{
	public class DesignViewModel: ObservableObject
	{
		#region Properties

		public DesignDocingViewModel DesignDocing { get; set; }

		#endregion Properties

		#region Fields

		private int _counter;



		#endregion Fields

		#region Constroctor

		public DesignViewModel(DevicesContainer devicesContainer)
		{
			_counter = 1;

			NewDashboradCommand = new RelayCommand(NewDashborad);

			DesignDocing = new DesignDocingViewModel(devicesContainer);
		}

		#endregion Constroctor

		#region Methods

		private void NewDashborad()
		{
			DesignDocing.AddDashboard(
				new DesignDashboardViewModel($"Dashboard {_counter++}"));
		}

		#endregion Methods

		#region Commands

		public RelayCommand NewDashboradCommand { get; private set; }

		#endregion Commands
	}
}
