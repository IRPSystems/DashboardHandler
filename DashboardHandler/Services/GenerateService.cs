

using DashboardHandler.Models;

namespace DashboardHandler.Services
{
	public class GenerateService
	{
		private Generate_Xaml_Service _generate_Xaml;

		public void Generate(DesignDiagramData DesignDiagram) 
		{
			_generate_Xaml.CreateXaml(DesignDiagram);
		}

		

		
	}
}
