

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dashboard.Views;
using DeviceHandler.Models;
using Microsoft.Win32;

namespace Dashboard.ViewModels
{
	public class DisplayViewModel: ObservableObject
	{
		public DisplayDokcingViewModel DisplayDokcing { get; set; }

		private DevicesContainer _devicesContainer;

		public DisplayViewModel(DevicesContainer devicesContainer)
		{
			_devicesContainer = devicesContainer;

			LoadDashboradCommand = new RelayCommand(LoadDashborad);

			DisplayDokcing = new DisplayDokcingViewModel();
		}

		private void LoadDashborad()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Dashboard file (*.db)|*.db";
			bool? result = openFileDialog.ShowDialog();
			if (result != true)
				return;

			DisplayDashboardViewModel vm = 
				new DisplayDashboardViewModel(
					openFileDialog.FileName,
					_devicesContainer);


			DisplayDokcing.AddDashboard(vm, new DisplayDashboardView());

		}

		public RelayCommand LoadDashboradCommand { get; private set; }
	}
}
