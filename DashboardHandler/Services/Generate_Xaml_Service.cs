

using DashboardHandler.Models.ToolsDesign;
using DashboardHandler.Models;
using System.IO;
using Services.Services;

namespace DashboardHandler.Services
{
	public class Generate_Xaml_Service
	{

		private int toolsCounter;

		public void CreateXaml(DesignDiagramData DesignDiagram)
		{
			toolsCounter = 1;

			string cleanName = CleanTextService.GetCleanName(DesignDiagram.Name);

			string xamlText =
				$"<UserControl x:Class=\"DashboardHandler.Views.{cleanName}View\"\r\n" +
				"             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
				"             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n" +
				"             xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \r\n" +
				"             xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" \r\n" +
				//"             xmlns:i=\"http://schemas.microsoft.com/xaml/behaviors\"\r\n\" +" +
				//"             xmlns:iconPacks=\"http://metro.mahapps.com/winfx/xaml/iconpacks\"\r\n\" +" +
				"             xmlns:mah=\"clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro\" \r\n" +
				"             xmlns:local=\"clr-namespace:DashboardHandler.Views\"\r\n" +
				"             mc:Ignorable=\"d\" \r\n" +
				"             d:DesignHeight=\"450\" d:DesignWidth=\"800\">\r\n\r\n";

			xamlText += "\t<Canvas>\r\n\r\n";

			AddControlsToCanvas(DesignDiagram.ToolList, ref xamlText);

			xamlText += "    </Canvas>\r\n</UserControl>\r\n";

			using (StreamWriter streamWriter = new StreamWriter(
				$"C:\\Users\\smadar\\Documents\\Dashborads\\{cleanName}View.xaml"))
			{
				streamWriter.Write(xamlText);
			}

		}

		private void AddControlsToCanvas(
			List<DesignToolBase> ToolList,
			ref string xamlText)
		{
			foreach (DesignToolBase tool in ToolList)
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


				switch (tool.GetType().Name)
				{
					case "DesignToolSwitch":
						AddTool_Switch(toolName, ref xamlText);
						break;
					case "DesignToolComboBox":
						AddTool_ComboBox(toolName, ref xamlText);
						break;
					case "DesignToolTextBox":
						AddTool_TextBox(toolName, ref xamlText);
						break;
					case "DesignToolLed":
						AddTool_Led(toolName, ref xamlText);
						break;
					case "DesignToolGauge":
						AddTool_Gauge(toolName, ref xamlText);
						break;
				}
			}
		}

		private void AddTool_Switch(
			string toolName,
			ref string xamlText)
		{
			xamlText +=
				"        <mah:ToggleSwitch IsOn=\"{Binding " + toolName + ".IsChecked}\"\r\n" +
				"                          OffContent=\"{Binding " + toolName + ".OffDescription}\"\r\n" +
				"                          OnContent=\"{Binding " + toolName + ".OnDescription}\" \r\n" +
				"                          Canvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"\r\n" +
				"                          Width=\"{Binding " + toolName + ".Width}\" Height=\"{Binding " + toolName + ".Height}\" />\r\n\r\n";
		}

		private void AddTool_ComboBox(
			string toolName,
			ref string xamlText)
		{
			xamlText +=
				"        <ComboBox ItemsSource=\"{ Binding " + toolName + ".Items}\"\r\n" +
				"                  SelectedItem = \"{Binding " + toolName + ".SelectedItem}\"\r\n" +
				"                  Canvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"\r\n" +
				"                  Width=\"{Binding " + toolName + ".Width}\" Height=\"{Binding " + toolName + ".Height}\" />\r\n\r\n";
		}

		private void AddTool_TextBox(
			string toolName,
			ref string xamlText)
		{
			xamlText +=
				"        <TextBox Text=\"{ Binding " + toolName + ".Text}\"\r\n" +
				"                 Canvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"\r\n" +
				"                 Width=\"{Binding " + toolName + ".Width}\" Height=\"{Binding " + toolName + ".Height}\" />\r\n\r\n";
		}

		private void AddTool_Led(
			string toolName,
			ref string xamlText)
		{
			xamlText +=
				"\t\t<Grid Width=\"{ Binding " + toolName + ".ParentNode.UnitWidth}\"\r\n" +
				"\t\t\tHeight = \"{Binding " + toolName + ".ParentNode.UnitHeight}\"\r\n" +
				"\t\t\tCanvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"\r\n" +
				"\t\t\tBackground=\"Transparent\" >\r\n\r\n" +
				"\t\t\t<Ellipse Stroke=\"{DynamicResource MahApps.Brushes.Gray1}\" />\r\n" +
				"\t\t\t<Ellipse Stroke=\"{DynamicResource MahApps.Brushes.Gray1}\"\r\n" +
				"\t\t\t\t\t\tMargin = \"3\" >\r\n" +
				"\t\t\t\t<Ellipse.Resources >\r\n" +
				"\t\t\t\t\t<Style TargetType = \"Ellipse\" >\r\n" +
				"\t\t\t\t\t\t<Style.Triggers >\r\n" +
				"\t\t\t\t\t\t\t<DataTrigger Binding = \"{Binding " + toolName + ".IsChecked}\" Value = \"True\" >\r\n" +
				"\t\t\t\t\t\t\t\t<Setter Property = \"Fill\" Value = \"{Binding " + toolName + ".OnColor}\" />\r\n" +
				"\t\t\t\t\t\t\t</DataTrigger >\r\n" +
				"\t\t\t\t\t\t\t<DataTrigger Binding = \"{Binding IsChecked}\" Value = \"False\" >\r\n" +
				"\t\t\t\t\t\t\t\t<Setter Property = \"Fill\" Value = \"{Binding " + toolName + ".OffColor}\" />\r\n" +
				"\t\t\t\t\t\t\t</DataTrigger >\r\n" +
				"\t\t\t\t\t\t</Style.Triggers >\r\n" +
				"\t\t\t\t\t</Style >\r\n" +
				"\t\t\t\t</Ellipse.Resources >\r\n" +
				"\t\t\t</Ellipse >\r\n" +
				"\t\t</Grid >\r\n\r\n";
		}

		private void AddTool_Gauge(
			string toolName,
			ref string xamlText)
		{
			xamlText +=
				"        <TextBox Text=\"{ Binding " + toolName + ".Text}\"\r\n" +
				"                 Canvas.Top=\"{Binding " + toolName + ".OffsetY}\" Canvas.Left=\"{Binding " + toolName + ".OffsetX}\"\r\n" +
				"                 Width=\"{Binding " + toolName + ".Width}\" Height=\"{Binding " + toolName + ".Height}\" />\r\n\r\n";
		}
	}
}
