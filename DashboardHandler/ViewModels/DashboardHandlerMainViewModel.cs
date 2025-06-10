

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DeviceCommunicators.Models;
using DeviceCommunicators.Services;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using Entities.Enums;
using System.Collections.ObjectModel;
using System.Reflection;

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



		#endregion Fields

		#region Constructor

		public DashboardHandlerMainViewModel() 
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			Version = assembly.GetName().Version.ToString();

			InitDeviceContainer();

			ChangeDarkLightCommand = new RelayCommand(ChangeDarkLight);

			Display = new DisplayViewModel(DevicesContainer);
			Design = new DesignViewModel(DevicesContainer);

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

		#endregion Methods

		#region Commands

		public RelayCommand ChangeDarkLightCommand { get; private set; }


		#endregion Commands
	}
}
