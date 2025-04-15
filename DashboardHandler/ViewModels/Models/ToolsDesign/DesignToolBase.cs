
using CommunityToolkit.Mvvm.ComponentModel;
using DeviceCommunicators.Models;
using System.Collections.ObjectModel;

namespace DashboardHandler.ViewModels.Models.ToolsDesign
{
    public class DesignToolBase: ObservableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<DeviceParameterData> ParameterData { get; set; }

    }
}
