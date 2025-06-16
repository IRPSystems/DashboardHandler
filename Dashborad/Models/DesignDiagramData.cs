
using CommunityToolkit.Mvvm.ComponentModel;
using Dashboard.Models.ToolsDesign;
using DeviceHandler.Models;

namespace Dashboard.Models
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
