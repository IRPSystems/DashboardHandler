

using CommunityToolkit.Mvvm.Input;
using DeviceCommunicators.Models;
using DeviceHandler.Models;
using Newtonsoft.Json;

namespace DashboardHandler.Models.ToolsDesign
{
    public class DesignToolTextBox : DesignToolBase
    {
		public DeviceParameterData Parameter { get; set; }

		public DesignToolTextBox() 
		{
			SendParameterCommand = new RelayCommand(SendParameter);
		}

		public override void SetParameter(DeviceParameterData parameter)
		{
			Parameter = parameter;
		}

		public override void Init(DevicesContainer devicesContainer)
		{
			GetRealParameter(devicesContainer);
		}

		protected override void GetRealParameter(DevicesContainer devicesContainer)
		{
			Parameter = GetRealParam(
					Parameter,
					devicesContainer);
		}

		private void SendParameter()
		{

		}


		[JsonIgnore]
		public RelayCommand SendParameterCommand { get; private set; }
	}
}
