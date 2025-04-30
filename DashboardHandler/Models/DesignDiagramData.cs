
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardHandler.Models.ToolsDesign;

namespace DashboardHandler.Models
{
	public class DesignDiagramData: ObservableObject
	{
		public string Name { get; set; }
		public List<DesignToolBase> ToolList { get; set; }
		public string FilePath { get; set; }

		public DesignDiagramData() 
		{
			ToolList = new List<DesignToolBase>();
		}
	}
}
