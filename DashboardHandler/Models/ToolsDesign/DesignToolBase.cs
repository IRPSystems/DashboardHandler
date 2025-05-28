
using CommunityToolkit.Mvvm.ComponentModel;
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
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


		protected DeviceParameterData GetRealParam(
			DeviceParameterData originalParam,
			DevicesContainer devicesContainer)
		{
			if (originalParam == null)
				return null;

			if (devicesContainer.TypeToDevicesFullData.ContainsKey(originalParam.DeviceType) == false)
				return null;

			DeviceFullData deviceFullData =
				devicesContainer.TypeToDevicesFullData[originalParam.DeviceType];
			if (deviceFullData == null)
				return null;

			DeviceParameterData actualParam = null;
			if (originalParam is MCU_ParamData mcuParam)
			{
				actualParam =
					deviceFullData.Device.ParemetersList.ToList().Find((p) =>
						((MCU_ParamData)p).Cmd == mcuParam.Cmd);
			}
			else
			{
				actualParam =
					deviceFullData.Device.ParemetersList.ToList().Find((p) =>
						p.Name == originalParam.Name);
			}

			return actualParam;
		}


		public abstract void SetParameter(DeviceParameterData parameter);
		public abstract void Init(DevicesContainer devicesContainer);

		protected abstract void GetRealParameter(DevicesContainer devicesContainer);

	}
}
