
using DeviceCommunicators.Enums;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using DeviceHandler.Models.DeviceFullDataModels;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Dashboard.Models.ToolsDesign
{
    public class DesignToolMonitorList : DesignToolBase
    {
        public ObservableCollection<DeviceParameterData> ParametersList { get; set; }

		public DesignToolMonitorList()
		{
			ParametersList = new ObservableCollection<DeviceParameterData>();
			ParametersList.CollectionChanged += ParametersList_CollectionChanged;
		}

		private void ParametersList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged(nameof(ParametersList));
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			ParametersList.Add(parameter);
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);

			DeviceFullData deviceFullData = devicesContainer.DevicesFullDataList[0];
			foreach (DeviceParameterData parameter in ParametersList)
			{
				deviceFullData.ParametersRepository.Add(
						parameter,
						DeviceHandler.Enums.RepositoryPriorityEnum.Medium,
						Callback);
			}
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

		private void Callback(DeviceParameterData param, CommunicatorResultEnum result, string resultDescription)
		{

		}
	}
}
