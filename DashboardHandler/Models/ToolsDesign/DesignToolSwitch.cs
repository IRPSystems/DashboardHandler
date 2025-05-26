
using DeviceCommunicators.Models;
using DeviceHandler.Models;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolSwitch: DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public bool IsChecked { get; set; }
        public string OnDescription { get; set; }
        public string OffDescription { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{

		}
	}
}
