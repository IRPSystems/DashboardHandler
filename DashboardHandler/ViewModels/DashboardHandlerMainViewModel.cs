

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dashboard.ViewModels;
using DeviceCommunicators.Models;
using DeviceCommunicators.Services;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using Entities.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace DashboardHandler.ViewModels
{
	public class DashboardHandlerMainViewModel: ObservableObject
	{
		#region Properties

		public string Version { get; set; }
		public DisplayViewModel Display { get; set; }
		public DesignViewModel Design { get; set; }

		public bool IsLightTheme { get; set; }

		public DevicesContainer DevicesContainer { get; set; }

		#endregion Properties

		#region Fields

		private ResourceDictionary _appResources;

		#endregion Fields

		#region Constructor

		public DashboardHandlerMainViewModel() 
		{
			_appResources = Application.Current.Resources;

			Assembly assembly = Assembly.GetExecutingAssembly();
			Version = assembly.GetName().Version.ToString();

			InitDeviceContainer();

			ChangeDarkLightCommand = new RelayCommand(ChangeDarkLight);
			ClosingCommand = new RelayCommand<CancelEventArgs>(Closing);

			Display = new DisplayViewModel(DevicesContainer);
			Design = new DesignViewModel(DevicesContainer, _appResources);

			IsLightTheme = false;
			ChangeDarkLight();
		}

		#endregion Constructor

		#region Methods

		private void InitDeviceContainer()
		{
			DevicesContainer = new DevicesContainer();
			DevicesContainer.DevicesFullDataList = new ObservableCollection<DeviceFullData>();
			DevicesContainer.DevicesList = new ObservableCollection<DeviceData>();
			DevicesContainer.TypeToDevicesFullData = new Dictionary<DeviceTypesEnum, DeviceFullData>();

			ReadDevicesFileService reader = new ReadDevicesFileService();
			ObservableCollection<DeviceData> devicesList = new ObservableCollection<DeviceData>();
			reader.ReadFromMCUJson(
				@"C:\Users\smadar\Documents\Stam\Json files\_param_defaults_All.json",
				devicesList,
				"MCU",
				DeviceTypesEnum.MCU);

			if (devicesList[0].ParemetersList == null || devicesList[0].ParemetersList.Count == 0)
			{
				return;
			}

			DevicesContainer.DevicesList.Add(devicesList[0]);

			if (DevicesContainer.DevicesFullDataList.Count == 0)
			{
				DeviceFullData fullData = new DeviceFullData_MCU(devicesList[0], false);
				DevicesContainer.DevicesFullDataList.Add(fullData);
				DevicesContainer.TypeToDevicesFullData.Add(DeviceTypesEnum.MCU, fullData);
				fullData.Init("TrueDriveManager", null);
				fullData.InitCheckConnection();
			}
			else
			{
				DeviceFullData fullData = DevicesContainer.DevicesFullDataList[0];
				fullData.Device = devicesList[0];
			}


			WeakReferenceMessenger.Default.Send(new SETUP_UPDATEDMessage());
		}

		private void ChangeDarkLight()
		{
			IsLightTheme = !IsLightTheme;

			App.ChangeDarkLight(IsLightTheme);

			if(Design != null) 
				Design.ChangeDarkLight();
		}

		private void Closing(CancelEventArgs e)
		{
			bool isExit = Design.Dispose();
			if(isExit == false) 
				e.Cancel = true;
		}

		#endregion Methods

			#region Commands

		public RelayCommand ChangeDarkLightCommand { get; private set; }
		public RelayCommand<CancelEventArgs> ClosingCommand { get; private set; }


		#endregion Commands
	}
}
