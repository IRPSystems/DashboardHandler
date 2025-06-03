

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
    public class DesignToolTextBox : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }

		private DevicesContainer _devicesContainer;

		public DesignToolTextBox() 
		{
			SendParameterCommand = new RelayCommand(SendParameter);
			TextBox_TextChangedCommand = new RelayCommand(TextBox_TextChanged);
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
					item.Name == nameof(TextBox_TextChangedCommand))
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

			if (Parameter.Value == null)
				return;

			double value;
			if(Parameter.Value is string str)
			{
				bool res = double.TryParse(str, out value);
				if (res == false)
					return;
			}
			else
				value = Convert.ToDouble(Parameter.Value);

			

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

		private void TextBox_TextChanged()
		{
			Background = Application.Current.FindResource("MahApps.Brushes.Accent4") as SolidColorBrush;
		}


		[JsonIgnore]
		public RelayCommand SendParameterCommand { get; private set; }
		public RelayCommand TextBox_TextChangedCommand { get; private set; }
	}
}
