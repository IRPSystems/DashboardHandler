
using DeviceCommunicators.Models;
using DeviceHandler.Plots;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolRegister : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public RegisterViewModel Register { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}
	}
}
