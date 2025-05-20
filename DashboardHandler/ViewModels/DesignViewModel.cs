
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeviceHandler.Models;
using Microsoft.Win32;
using System.IO;

namespace DashboardHandler.ViewModels
{
	public class DesignViewModel: ObservableObject
	{
		#region Properties

		public DesignDocingViewModel DesignDocing { get; set; }

		#endregion Properties

		#region Fields

		private int _counter;
		private PropertyGridViewModel _propertyGrid;
		private StencilViewModel _stencil;

		private List<DesignDashboardViewModel> _designDashboardList;

		#endregion Fields

		#region Constroctor

		public DesignViewModel(DevicesContainer devicesContainer)
		{
			_counter = 1;

			NewDashboradCommand = new RelayCommand(NewDashborad);
			LoadDashboradCommand = new RelayCommand(LoadDashborad);

			_propertyGrid = new PropertyGridViewModel();
			_stencil = new StencilViewModel();
			DesignDocing = new DesignDocingViewModel(
				devicesContainer, 
				_propertyGrid, 
				_stencil);

			_designDashboardList = new List<DesignDashboardViewModel>();
		}

		#endregion Constroctor

		#region Methods

		public void ChangeDarkLight()
		{
			if (DesignDocing != null)
				DesignDocing.Refresh();

			if(_propertyGrid != null)
				_propertyGrid.ChangeDarkLight();

			if(_stencil != null)
				_stencil.ChangeDarkLight();

			foreach (var dashboard in _designDashboardList)
			{
				dashboard.ChangeDarkLight();
			}
		}

		private void NewDashborad()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Dashboard file (*.db)|*.db";
			bool? result = saveFileDialog.ShowDialog();
			if (result != true)
				return;

			string dashboardName = Path.GetFileName(saveFileDialog.FileName);
			dashboardName = dashboardName.Replace(".db", string.Empty);

			DesignDashboardViewModel vm = new DesignDashboardViewModel(
					dashboardName,
					saveFileDialog.FileName,
					_propertyGrid);

			DesignDocing.AddDashboard(vm);
			_designDashboardList.Add(vm);

			vm.Save();
		}

		private void LoadDashborad()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Dashboard file (*.db)|*.db";
			bool? result = openFileDialog.ShowDialog();
			if (result != true) 
				return;

			string dashboardName = Path.GetFileName(openFileDialog.FileName);
			dashboardName = dashboardName.Replace(".db", string.Empty);

			DesignDashboardViewModel vm = new DesignDashboardViewModel(
					dashboardName,
					openFileDialog.FileName,
					_propertyGrid);

			vm.Open(openFileDialog.FileName);

			DesignDocing.AddDashboard(vm);
			_designDashboardList.Add(vm);
		}

		#endregion Methods

		#region Commands


		public RelayCommand NewDashboradCommand { get; private set; }
		public RelayCommand LoadDashboradCommand { get; private set; }


		#endregion Commands
	}
}
