
using CommunityToolkit.Mvvm.ComponentModel;
using Controls.Interfaces;
using DashboardHandler.Models;
using DashboardHandler.Models.ToolsDesign;
using DeviceHandler.Models;
using Newtonsoft.Json;
using Services.Services;
using System.IO;

namespace DashboardHandler.ViewModels
{
    public class DisplayDashboardViewModel: ObservableObject, IDocumentVM
	{
		public DesignDiagramData DesignDiagram { get; set; }
		public double CanvasHeight { get; set; }
		public double CanvasWidth { get; set; }

		public string Name
		{
			get
			{
				if (DesignDiagram == null)
					return null;
				return DesignDiagram.Name;
			}
			set { }
		}

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

				double maxBottom = 0;
				double maxRight = 0;
				foreach (DesignToolBase tool in DesignDiagram.ToolList)
				{
					tool.Init(_devicesContainer);

					double toolBottom = GetToolBottom(tool);
					if (toolBottom > maxBottom) 
						maxBottom = toolBottom;

					double toolRight = GetToolRight(tool);
					if (toolRight > maxRight)
						maxRight = toolRight;
				}

				CanvasHeight = maxBottom;
				CanvasWidth = maxRight;
			}
			catch (Exception ex) 
			{
				LoggerService.Error(this, "Failed to load the file", "Error", ex);
			}
		}

		private double GetToolBottom(DesignToolBase tool)
		{
			double bottom = tool.OffsetY + tool.Height;
			return bottom;
		}

		private double GetToolRight(DesignToolBase tool)
		{
			double right = tool.OffsetX + tool.Width;
			return right;
		}
	}
}
