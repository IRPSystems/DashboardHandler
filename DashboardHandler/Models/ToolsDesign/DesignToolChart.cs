
using DeviceCommunicators.Models;
using DeviceHandler.Plots;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolChart : DesignToolBase
    {
		public ObservableCollection<DeviceParameterData> ParametersList { get; set; }
		public LineChartViewModel Chart { get; set; }

		public DesignToolChart()
		{
			ParametersList = new ObservableCollection<DeviceParameterData>();
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			ParametersList.Add(parameter);
		}
	}
}
