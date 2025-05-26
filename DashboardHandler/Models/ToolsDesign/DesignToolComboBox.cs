
using DeviceCommunicators.Models;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolComboBox : DesignToolBase
	{
		public DeviceParameterData Parameter { get; set; }

        public object SelectedItem { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}
	}
}
