
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardHandler.Models.ToolsDesign;

namespace DashboardHandler.ViewModels
{
    public class PropertyGridViewModel: ObservableObject
    {
        public DesignToolBase SelectedNode { get; set; }
	}
}
