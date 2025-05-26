

using DashboardHandler.Models;

namespace DashboardHandler.Services
{
	public class GenerateService
	{
		private Generate_Xaml_Service _generate_Xaml;
		private Generate_ViewModel_Service _generate_ViewModel;

		public GenerateService()
		{
			_generate_Xaml = new Generate_Xaml_Service();
			_generate_ViewModel = new Generate_ViewModel_Service();
		}

		public void Generate(DesignDiagramData DesignDiagram) 
		{

			_generate_Xaml.CreateXaml(DesignDiagram);
			_generate_ViewModel.CreateViewModel(DesignDiagram);
		}

		

		
	}
}
