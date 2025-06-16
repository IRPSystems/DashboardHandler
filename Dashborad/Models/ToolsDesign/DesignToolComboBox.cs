
using CommunityToolkit.Mvvm.Input;
using DeviceCommunicators.Enums;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using Entities.Enums;
using Entities.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Dashboard.Models.ToolsDesign
{
    public class DesignToolComboBox : DesignToolBase
	{
		public DeviceParameterData Parameter { get; set; }
		public DropDownParamData SelectedItem { get; set; }

		private DevicesContainer _devicesContainer;

		public DesignToolComboBox() 
		{
			SendParameterCommand = new RelayCommand(SendParameter);
			ComboBox_SelectionChangedCommand = new RelayCommand(ComboBox_SelectionChanged);
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
					item.Name == nameof(ComboBox_SelectionChangedCommand))
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
			if (deviceFullData.DeviceCommunicator == null ||
				deviceFullData.DeviceCommunicator.IsInitialized == false)
			{
				return;
			}

			double value;
			double.TryParse(SelectedItem.Value, out value);

			deviceFullData.DeviceCommunicator.SetParamValue(
				Parameter,
				value,
				Callback);
		}

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string errDescription)
		{
			if (result == CommunicatorResultEnum.OK)
				Background = Application.Current.FindResource("MahApps.Brushes.ThemeBackground") as SolidColorBrush;
		}

		private void ComboBox_SelectionChanged()
		{
			Background = Application.Current.FindResource("MahApps.Brushes.Accent4") as SolidColorBrush;
		}

		[JsonIgnore]
		public RelayCommand SendParameterCommand { get; private set; }
		public RelayCommand ComboBox_SelectionChangedCommand { get; private set; }
	}
}
