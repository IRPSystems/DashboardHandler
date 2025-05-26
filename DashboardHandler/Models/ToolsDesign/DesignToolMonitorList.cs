
using DeviceCommunicators.Models;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolMonitorList : DesignToolBase
    {
        public ObservableCollection<DeviceParameterData> ParametersList { get; set; }

		public DesignToolMonitorList()
		{
			ParametersList = new ObservableCollection<DeviceParameterData>();
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			ParametersList.Add(parameter);
		}
	}
}
