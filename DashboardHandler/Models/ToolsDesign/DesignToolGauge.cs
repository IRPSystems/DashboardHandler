
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Plots;
using Newtonsoft.Json;

namespace DashboardHandler.Models.ToolsDesign
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
			Gauge = new Speedometer(Parameter as MCU_ParamData);
		}
	}
}
