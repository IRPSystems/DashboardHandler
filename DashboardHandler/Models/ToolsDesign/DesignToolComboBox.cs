
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using Entities.Models;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolComboBox : DesignToolBase
	{
		public DeviceParameterData Parameter { get; set; }

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{

		}
	}
}
