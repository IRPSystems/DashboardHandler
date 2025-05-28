
using DeviceCommunicators.Enums;
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using DeviceHandler.Plots;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolRegister : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }

		[JsonIgnore]
		public RegisterViewModel Register { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);

			Register = new RegisterViewModel(Parameter as MCU_ParamData);

			DeviceFullData deviceFullData = devicesContainer.DevicesFullDataList[0];
			deviceFullData.ParametersRepository.Add(
					Parameter,
					DeviceHandler.Enums.RepositoryPriorityEnum.Medium,
					Callback);
		}

		public override List<string> GetHideProperties()
		{
			List<string> propertyDescriptorsList = base.GetHideProperties();

			List<string> localList = new List<string>();
			PropertyDescriptorCollection p = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor item in p)
			{
				if (item.Name == nameof(Register))
				{
					localList.Add(item.Name);
				}
			}

			propertyDescriptorsList.AddRange(localList);

			return propertyDescriptorsList;
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			Parameter = GetRealParam(
					Parameter,
					devicesContainer);
		}

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string resultDescription)
		{

		}
	}
}
