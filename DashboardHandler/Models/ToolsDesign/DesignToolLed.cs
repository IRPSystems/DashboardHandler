
using DeviceCommunicators.Models;
using System.Windows.Media;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolLed : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }
		public bool IsChecked { get; set; }
        public Brush OnColor { get; set; }
        public Brush OffColor { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}
	}
}
