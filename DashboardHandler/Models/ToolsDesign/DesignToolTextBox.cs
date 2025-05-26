

using DeviceCommunicators.Models;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolTextBox : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public string Text { get; set; }
    }
}
