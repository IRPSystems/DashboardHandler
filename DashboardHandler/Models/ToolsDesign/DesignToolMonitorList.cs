
using DeviceCommunicators.Models;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolMonitorList : DesignToolBase
    {
        public ObservableCollection<DeviceParameterData> ParemetersList { get; set; }
    }
}
