
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
        public ObservableCollection<DeviceParameterData> ParameterData { get; set; }

        public NodeViewModel ParentNode { get; set; }

        public virtual object Clone()
        {
			DesignToolBase designToolBase = 
                MemberwiseClone() as DesignToolBase;

			designToolBase.ParameterData = new ObservableCollection<DeviceParameterData>();
			foreach (var parameterData in ParameterData)
            {
				designToolBase.ParameterData.Add(
                    parameterData.Clone() as DeviceParameterData);
			}

            return designToolBase;

		}

    }
}
