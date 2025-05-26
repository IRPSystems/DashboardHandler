
using DashboardHandler.Models;
using DashboardHandler.Models.ToolsDesign;
using Services.Services;
using Syncfusion.UI.Xaml.Diagram;
using System.IO;
using System.Xml;

namespace DashboardHandler.Services
{
	public class Generate_ViewModel_Service
	{

		public void CreateViewModel(DesignDiagramData designDiagram)
		{

			string cleanName = CleanTextService.GetCleanName(designDiagram.Name);

			string indent = "\t";

			string fileText =
				"using CommunityToolkit.Mvvm.ComponentModel;\r\n" +
				"using DashboardHandler.Models.ToolsDesign;\r\n" +
				"\r\n" +
				"namespace DashboardHandler.ViewModels\r\n" +
				"{\r\n" +
				$"{indent}public class {cleanName}ViewModel: ObservableObject\r\n" +
				$"{indent}{{\r\n";



			AddPropertiesFromDiagram(
				ref fileText,
				indent,
				designDiagram);

			AddContructor(
				ref fileText,
				indent,
				designDiagram,
				cleanName);

			fileText +=
				$"{indent}}}\r\n" +
				"}";


			using (StreamWriter streamWriter = new StreamWriter(
				$"C:\\Users\\smadar\\Documents\\Dashborads\\{cleanName}ViewModel.cs"))
			{
				streamWriter.Write(fileText);
			}
		}

		private void AddPropertiesFromDiagram(
			ref string fileText,
			string indent,
			DesignDiagramData designDiagram)
		{
			indent = indent + "\t";

			int toolsCounter = 1;

			foreach (DesignToolBase tool in designDiagram.ToolList)
			{
				

				string toolName = string.Empty;
				if (string.IsNullOrEmpty(tool.Name))
				{
					string name = tool.GetType().Name;
					name = name.Replace("DesignTool", string.Empty);
					name += toolsCounter.ToString();
					toolsCounter++;
					toolName = CleanTextService.GetCleanName(name);
				}
				else
				{
					toolName = CleanTextService.GetCleanName(tool.Name);
				}

				fileText +=
					$"{indent}public {tool.GetType().Name} {toolName} {{ get; set; }}\r\n";
			}

			fileText += "\r\n";
		}

		private void AddContructor(
			ref string fileText,
			string indent,
			DesignDiagramData designDiagram,
			string cleanName)
		{
			indent = indent + "\t";

			fileText +=
					$"{indent}public {cleanName}ViewModel()\r\n" +
					$"{indent}{{\r\n";


			int toolsCounter = 1;

			foreach (DesignToolBase tool in designDiagram.ToolList)
			{
				AddToolSettingsProperties(
					tool,
					ref fileText,
					ref toolsCounter,
					indent);
			}



			fileText +=
					$"{indent}}}\r\n\r\n";
		}

		private void AddToolSettingsProperties(
			DesignToolBase tool,
			ref string fileText,
			ref int toolsCounter,
			string indent)
		{
			indent = indent + "\t";

			string toolName = string.Empty;
			if (string.IsNullOrEmpty(tool.Name))
			{
				string name = tool.GetType().Name;
				name = name.Replace("DesignTool", string.Empty);
				name += toolsCounter.ToString();
				toolsCounter++;
				toolName = CleanTextService.GetCleanName(name);
			}
			else
			{
				toolName = CleanTextService.GetCleanName(tool.Name);
			}

			string paramsSettingStr =
				tool.GetParamsSettingStr(toolName, indent);

			fileText += paramsSettingStr + "\r\n";
		}
	}
}
