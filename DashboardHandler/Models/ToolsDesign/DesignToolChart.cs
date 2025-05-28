
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Plots;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolChart : DesignToolBase
    {
		public ObservableCollection<DeviceParameterData> ParametersList { get; set; }

		[JsonIgnore]
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

		public override List<string> GetHideProperties()
		{
			List<string> propertyDescriptorsList = base.GetHideProperties();

			List<string> localList = new List<string>();
			PropertyDescriptorCollection p = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor item in p)
			{
				if (item.Name == nameof(Chart))
				{
					localList.Add(item.Name);
				}
			}

			propertyDescriptorsList.AddRange(localList);

			return propertyDescriptorsList;
		}
	}
}
