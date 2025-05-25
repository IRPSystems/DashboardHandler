

using DashboardHandler.Models;

namespace DashboardHandler.Services
{
	public class GenerateService
	{
		private Generate_Xaml_Service _generate_Xaml;

		public GenerateService()
		{
			_generate_Xaml = new Generate_Xaml_Service();
		}

		public void Generate(DesignDiagramData DesignDiagram) 
		{

			_generate_Xaml.CreateXaml(DesignDiagram);
		}

		

		
	}
}
