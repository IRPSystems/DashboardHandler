
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using System.Collections.ObjectModel;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolMonitorList : DesignToolBase
    {
        public ObservableCollection<DeviceParameterData> ParametersList { get; set; }

		public DesignToolMonitorList()
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
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			for (int i = 0; i < ParametersList.Count; i++)
			{
				ParametersList[i] = GetRealParam(
					ParametersList[i],
					devicesContainer);
			}
		}
	}
}
