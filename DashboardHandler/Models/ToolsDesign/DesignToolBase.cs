
using CommunityToolkit.Mvvm.ComponentModel;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public abstract class DesignToolBase: ObservableObject, ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public NodeViewModel ParentNode { get; set; }

        public double OffsetX { get; set; }
		public double OffsetY { get; set; }

		public double Width { get; set; }
		public double Height { get; set; }

		public DesignToolBase()
        {
			GetHideProperties();
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

		public virtual List<string> GetHideProperties()
		{
			List<string> propertyDescriptorsList = new List<string>();

			PropertyDescriptorCollection p = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor item in p)
			{
				if (item.Name == nameof(ParentNode) ||
					item.Name == nameof(OffsetX) ||
					item.Name == nameof(OffsetY) ||
					item.Name == nameof(Width) ||
					item.Name == nameof(Height))
				{
					propertyDescriptorsList.Add(item.Name);
				}
			}

			return propertyDescriptorsList;
		}


		public abstract void SetParameter(DeviceParameterData parameter);
		public abstract void Init(DevicesContainer devicesContainer);

	}
}
