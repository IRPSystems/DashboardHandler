
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.Interfaces;
using Dashboard.Views;
using DeviceHandler.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Dashboard.ViewModels
{
	public class DesignViewModel: ObservableObject
	{
		#region Properties

		public DesignDokcingViewModel DesignDocing { get; set; }

		#endregion Properties

		#region Fields

		private PropertyGridViewModel _propertyGrid;
		private StencilViewModel _stencil;

		private List<DesignDashboardViewModel> _designDashboardList;

		private ResourceDictionary _appResoureces;

		#endregion Fields

		#region Constroctor

		public DesignViewModel(
			DevicesContainer devicesContainer,
			ResourceDictionary appResoureces)
		{
			_appResoureces = appResoureces;
			_appResoureces.MergedDictionaries.Add(
				new ResourceDictionary()
				{
					Source = new Uri("pack://application:,,,/Dashboard;component/Resources/NodesTemplates.xaml", UriKind.Absolute)
				});

			_appResoureces.MergedDictionaries.Add(
				new ResourceDictionary()
				{
					Source = new Uri("pack://application:,,,/Dashboard;component/Resources/SymbolsTemplates.xaml", UriKind.Absolute)
				});

			NewDashboradCommand = new RelayCommand(NewDashborad);
			LoadDashboradCommand = new RelayCommand(LoadDashborad);

			_propertyGrid = new PropertyGridViewModel(_appResoureces);
			_stencil = new StencilViewModel(_appResoureces);
			DesignDocing = new DesignDokcingViewModel(
				devicesContainer, 
				_propertyGrid, 
				_stencil);
			DesignDocing.DocumentClosedEvent += DesignDocing_DocumentClosedEvent;

			_designDashboardList = new List<DesignDashboardViewModel>();
		}

		#endregion Constroctor

		#region Methods

		public bool Dispose()
		{
			foreach (DesignDashboardViewModel db in _designDashboardList)
			{
				if(db.IsNeedSave)
				{
					bool isNotCancel = db.Dispose();
					if(isNotCancel == false)
						return false;
				}
			}

			return true;
		}

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
					_propertyGrid,
					_appResoureces);

			DesignDocing.AddDashboard(vm, new DesignDashboardView());
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
					_propertyGrid,
					_appResoureces);

			vm.Open(openFileDialog.FileName);

			DesignDocing.AddDashboard(vm, new DesignDashboardView());
			_designDashboardList.Add(vm);
		}

		private void DesignDocing_DocumentClosedEvent(IDocumentVM document)
		{
			if(document is DesignDashboardViewModel vm) 
				_designDashboardList.Remove(vm);
		}

		#endregion Methods

		#region Commands


		public RelayCommand NewDashboradCommand { get; private set; }
		public RelayCommand LoadDashboradCommand { get; private set; }


		#endregion Commands
	}
}
