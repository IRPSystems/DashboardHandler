
using DeviceCommunicators.Enums;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using Entities.Models;
using System.Windows.Media;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolLed : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public bool IsChecked { get; set; }
        public Brush OnColor { get; set; }
        public Brush OffColor { get; set; }
		public double OnValue { get; set; }
		public double OffValue { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);

			DeviceFullData deviceFullData = devicesContainer.DevicesFullDataList[0];
			deviceFullData.ParametersRepository.Add(
					Parameter,
					DeviceHandler.Enums.RepositoryPriorityEnum.Medium,
					Callback);
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			Parameter = GetRealParam(
					Parameter,
					devicesContainer);
		}

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string resultDescription)
		{
			if (param.Value == null)
				return;

			double value = 0;

			if (param.Value is string strVal)
			{
				if (param is IParamWithDropDown dropDown)
				{
					DropDownParamData item = dropDown.DropDown.Find((dd) => dd.Name == strVal);
					if (item == null)
						return;

					strVal = item.Value;
				}

				double.TryParse(strVal, out value);
			}
			else
			{
				value = Convert.ToDouble(param.Value);
			}

			if (value == OffValue)
				IsChecked = false;
			if (value == OnValue)
				IsChecked = true;
		}
	}
}
