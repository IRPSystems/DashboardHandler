
using DeviceCommunicators.Models;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolGauge : DesignToolBase
    {
        public DeviceParameterData Parameter { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}
	}
}
