﻿
using DeviceCommunicators.Enums;
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using DeviceHandler.Plots;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace Dashboard.Models.ToolsDesign
{
    public class DesignToolGauge : DesignToolBase
    {
        public DeviceParameterData Parameter { get; set; }

		[JsonIgnore]
		public Speedometer Gauge { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);
			Gauge = new Speedometer(Parameter as MCU_ParamData);

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
				if (item.Name == nameof(Gauge))
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
