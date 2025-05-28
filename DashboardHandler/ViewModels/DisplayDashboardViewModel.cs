
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardHandler.Models;
using DashboardHandler.Models.ToolsDesign;
using DeviceHandler.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using Services.Services;
using Syncfusion.Windows.PropertyGrid;
using System.Collections.ObjectModel;
using System.IO;

namespace DashboardHandler.ViewModels
{
    public class DisplayDashboardViewModel: ObservableObject
    {
		public DesignDiagramData DesignDiagram { get; set; }

		private DevicesContainer _devicesContainer;

		public DisplayDashboardViewModel(
			string path,
			DevicesContainer devicesContainer)
        {
			_devicesContainer = devicesContainer;

			LoadFile(path);
		}

        public void LoadFile(string path)
        {
			try
			{
				string jsonString = File.ReadAllText(path);

				JsonSerializerSettings settings = new JsonSerializerSettings();
				settings.Formatting = Formatting.Indented;
				settings.TypeNameHandling = TypeNameHandling.All;
				DesignDiagram = JsonConvert.DeserializeObject(jsonString, settings) as DesignDiagramData;

				foreach (DesignToolBase tool in DesignDiagram.ToolList)
				{
					tool.Init(_devicesContainer);
				}
			}
			catch (Exception ex) 
			{
				LoggerService.Error(this, "Failed to load the file", "Error", ex);
			}
		}

	}
}
