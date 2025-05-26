
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
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

		public override void Init(DevicesContainer devicesContainer)
		{
			Chart = new LineChartViewModel();

			foreach (DeviceParameterData parameter in ParametersList)
			{
				Chart.AddSeries(parameter as MCU_ParamData);
			}

		}
	}
}
