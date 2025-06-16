
using DeviceCommunicators.Enums;
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using DeviceHandler.Plots;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dashboard.Models.ToolsDesign
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
			GetRealParameter(devicesContainer);

			Chart = new LineChartViewModel();

			DeviceFullData deviceFullData = devicesContainer.DevicesFullDataList[0];

			foreach (DeviceParameterData parameter in ParametersList)
			{
				Chart.AddSeries(parameter as MCU_ParamData);

				deviceFullData.ParametersRepository.Add(
					parameter, 
					DeviceHandler.Enums.RepositoryPriorityEnum.Medium,
					Callback);
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

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			for(int i = 0; i < ParametersList.Count; i++)
			{
				ParametersList[i] = GetRealParam(
					ParametersList[i],
					devicesContainer);
			}
		}

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string resultDescription)
		{

		}
	}
}
