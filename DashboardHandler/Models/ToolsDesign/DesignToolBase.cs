
using CommunityToolkit.Mvvm.ComponentModel;
using DeviceCommunicators.Models;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolBase: ObservableObject, ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DeviceParameterData ParameterData { get; set; }

        public NodeViewModel ParentNode { get; set; }

        public double OffsetX { get; set; }
		public double OffsetY { get; set; }

		public double Width { get; set; }
		public double Height { get; set; }

		public DesignToolBase()
        {
		}

		public virtual object Clone()
        {
			return MemberwiseClone();
		}

		public virtual string GetParamsSettingStr(
			string toolName,
			string indent)
		{
			string str =
				$"{indent}{toolName} = new {GetType().Name}();\r\n" +
				$"{indent}{toolName}.Name = \"{Name}\";\r\n" +
				$"{indent}{toolName}.Description = \"{Description}\";\r\n" +
				$"{indent}{toolName}.OffsetX = {OffsetX};\r\n" +
				$"{indent}{toolName}.OffsetY = {OffsetY};\r\n" +
				$"{indent}{toolName}.Width = {Width};\r\n" +
				$"{indent}{toolName}.Height = {Height};\r\n";

			return str;
		}

    }
}
