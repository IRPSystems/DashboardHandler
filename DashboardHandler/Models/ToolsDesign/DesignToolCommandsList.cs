
using DeviceCommunicators.MCU;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolCommandsList : DesignToolBase
    {
		public ObservableCollection<DeviceParameterData> ParametersList { get; set; }

		[JsonIgnore]
		public ParamGroupViewModel ParamGroup { get; set; }

		public DesignToolCommandsList()
		{
			ParametersList = new ObservableCollection<DeviceParameterData>();
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			ParametersList.Add(parameter);
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			ParamGroup paramGroup = new ParamGroup();
			paramGroup.ParamList = new ObservableCollection<MCU_ParamData>();
			foreach (var param in ParametersList)
				paramGroup.ParamList.Add(param as MCU_ParamData);

			ParamGroup = new ParamGroupViewModel(devicesContainer, paramGroup, true);
		}

		public override List<string> GetHideProperties()
		{
			List<string> propertyDescriptorsList = base.GetHideProperties();

			List<string> localList = new List<string>();
			PropertyDescriptorCollection p = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor item in p)
			{
				if (item.Name == nameof(ParamGroup))
				{
					localList.Add(item.Name);
				}
			}

			propertyDescriptorsList.AddRange(localList);

			return propertyDescriptorsList;
		}
	}
}
