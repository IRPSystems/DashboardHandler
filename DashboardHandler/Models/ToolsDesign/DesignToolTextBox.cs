

using DeviceCommunicators.Models;
using DeviceHandler.Models;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolTextBox : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			Parameter = GetRealParam(
					Parameter,
					devicesContainer);
		}
	}
}
