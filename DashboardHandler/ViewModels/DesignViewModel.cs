
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeviceHandler.Models;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Diagnostics.Metrics;
using System.IO;
using System.Windows;

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

			ChangeDarkLightCommand = new RelayCommand(ChangeDarkLight);
			NewDashboradCommand = new RelayCommand(NewDashborad);
			SaveDashboradCommand = new RelayCommand(SaveDashborad);

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

		private void SaveDashborad()
		{
			foreach(var dashboard in _designDashboardList)
			{
				dashboard.Save();
			}
		}

		#endregion Methods

		#region Commands

		public RelayCommand ChangeDarkLightCommand { get; private set; }

		public RelayCommand NewDashboradCommand { get; private set; }
		public RelayCommand SaveDashboradCommand { get; private set; }


		#endregion Commands
	}
}
