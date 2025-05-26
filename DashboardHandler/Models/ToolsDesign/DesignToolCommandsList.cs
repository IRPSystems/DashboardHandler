
using DeviceCommunicators.Models;
using DeviceHandler.ViewModels;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolCommandsList : DesignToolBase
    {
		public ObservableCollection<DeviceParameterData> ParametersList { get; set; }
		public ParamGroupViewModel ParamGroup { get; set; }

		public DesignToolCommandsList()
		{
			ParametersList = new ObservableCollection<DeviceParameterData>();
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			ParametersList.Add(parameter);
		}
	}
}
