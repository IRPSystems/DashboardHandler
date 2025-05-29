
using CommunityToolkit.Mvvm.Input;
using DeviceCommunicators.Enums;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using Entities.Enums;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolSwitch: DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public bool IsChecked { get; set; }
        public string OnDescription { get; set; }
        public string OffDescription { get; set; }
		public double OnValue { get; set; }
		public double OffValue { get; set; }

		private DevicesContainer _devicesContainer;

		public DesignToolSwitch() 
		{
			SendParameterCommand = new RelayCommand(SendParameter);
			SwithStateChangedCommand = new RelayCommand(SwithStateChanged);
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override List<string> GetHideProperties()
		{
			List<string> propertyDescriptorsList = base.GetHideProperties();

			List<string> localList = new List<string>();
			PropertyDescriptorCollection p = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor item in p)
			{
				if (item.Name == nameof(SendParameterCommand) ||
					item.Name == nameof(SwithStateChangedCommand))
				{
					localList.Add(item.Name);
				}
			}

			propertyDescriptorsList.AddRange(localList);

			return propertyDescriptorsList;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			_devicesContainer = devicesContainer;

			GetRealParameter(devicesContainer);
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			Parameter = GetRealParam(
					Parameter,
					devicesContainer);
		}

		private void SendParameter()
		{
			DeviceFullData deviceFullData = 
				_devicesContainer.TypeToDevicesFullData[DeviceTypesEnum.MCU];
			if(deviceFullData.DeviceCommunicator == null ||
				deviceFullData.DeviceCommunicator.IsInitialized == false)
			{
				return;
			}

			double value = 0;
			if(IsChecked)
				value = OnValue;
			else
				value = OffValue;

			deviceFullData.DeviceCommunicator.SetParamValue(
				Parameter,
				value,
				Callback);

		}

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string errDescription)
		{
			if(result == CommunicatorResultEnum.OK)
				Background = Application.Current.FindResource("MahApps.Brushes.ThemeBackground") as SolidColorBrush;
		}

		private void SwithStateChanged()
		{
			Background = Application.Current.FindResource("MahApps.Brushes.Accent4") as SolidColorBrush;
		}


		[JsonIgnore]
		public RelayCommand SendParameterCommand { get; private set; }

		[JsonIgnore]
		public RelayCommand SwithStateChangedCommand { get; private set; }
	}
}
