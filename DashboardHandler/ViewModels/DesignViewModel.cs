
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeviceHandler.Models;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Diagnostics.Metrics;
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
			DesignDashboardViewModel vm = new DesignDashboardViewModel(
					$"Dashboard {_counter++}",
					_propertyGrid);

			DesignDocing.AddDashboard(vm);
			_designDashboardList.Add(vm);
		}

		#endregion Methods

		#region Commands

		public RelayCommand ChangeDarkLightCommand { get; private set; }

		public RelayCommand NewDashboradCommand { get; private set; }


		#endregion Commands
	}
}
