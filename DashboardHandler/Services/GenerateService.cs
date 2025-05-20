

using DashboardHandler.Models;
using DashboardHandler.Models.ToolsDesign;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DashboardHandler.Services
{
	public class GenerateService
	{
		public void Generate(DesignDiagramData DesignDiagram) 
		{
			CreateXaml(DesignDiagram);
		}

		private void CreateXaml(DesignDiagramData DesignDiagram)
		{
			string cleanName = GetCleanName(DesignDiagram.Name);

			string xamlText =
				$"<UserControl x:Class=\"DashboardHandler.Views.{cleanName}View\"\r\n" +
				"             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
				"             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n" +
				"             xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \r\n" +
				"             xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" \r\n" +
				//"             xmlns:i=\"http://schemas.microsoft.com/xaml/behaviors\"\r\n\" +" +
				//"             xmlns:iconPacks=\"http://metro.mahapps.com/winfx/xaml/iconpacks\"\r\n\" +" +
				"             xmlns:mah=\"clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro\" \r\n" +
				"             xmlns:local=\"clr-namespace:DashboardHandler.Views\"\r\n"  +
				"             mc:Ignorable=\"d\" \r\n" +
				"             d:DesignHeight=\"450\" d:DesignWidth=\"800\">";

			xamlText += "\t<Canvas>\r\n";

			AddControlsToCanvas(DesignDiagram.ToolList, xamlText);

			xamlText += "    </Canvas>\r\n</UserControl>\r\n";

		}

		private void AddControlsToCanvas(
			List<DesignToolBase> ToolList,
			string xamlText)
		{
			foreach (DesignToolBase tool in ToolList)
			{
				string toolName = GetCleanName(tool.Name);
				switch(tool.GetType().Name)
				{
					case "DesignToolSwitch":
						AddTool_Switch(
							(DesignToolSwitch)tool,
							toolName,
							xamlText);
						break;
				}
			}
		}

		private void AddTool_Switch(
			DesignToolSwitch switchTool,
			string toolName,
			string xamlText)
		{
			xamlText = 
				"        <mah:ToggleSwitch IsOn=\"{Binding " + toolName + ".IsChecked}\"\r\n" +
				"                          OffContent=\"{Binding " + toolName + ".OffDescription}\"\r\n" +
				"                          OnContent=\"{Binding " + toolName + ".OnDescription}\" \r\n" +
				"                          Canvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"" +
				"                          Width=\"{Binding " + toolName + ".Width}\" Height=\"{Binding " + toolName + ".Height}\" />";
		}

		private string GetCleanName(string name)
		{
			name = name.Replace(" ", string.Empty);
			name = name.Replace("-", "_");
			name = name.Replace(",", "_");
			name = name.Replace("/", "_");
			name = name.Replace("-", "_");
			name = name.Replace("(", "_");
			name = name.Replace(")", "_");

			return name;
		}
	}
}
