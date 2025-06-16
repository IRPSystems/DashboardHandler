using Controls.ViewModels;
using Dashboard.Views;
using DeviceHandler.Models;
using DeviceHandler.ViewModel;
using DeviceHandler.Views;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Dashboard.ViewModels
{
	public class DesignDokcingViewModel : DocumantsDokcingViewModel
	{
		#region Properties

		#endregion Properties

		#region Fields

		private ContentControl _parametrs;
		private ContentControl _stencil;
		private ContentControl _propertyGrid;

		#endregion Fields

		#region Constructor

		public DesignDokcingViewModel(
		DevicesContainer devicesContainer,
			PropertyGridViewModel propertyGrid,
			StencilViewModel stencil) :
			base("DesignDokcking", "Dashboard")
		{
			CreateWindows(devicesContainer, propertyGrid, stencil);
		}

		#endregion Constructor

		#region Methods

		private void CreateWindows(
			DevicesContainer devicesContainer,
			PropertyGridViewModel propertyGrid,
			StencilViewModel stencil)
		{
			ParametersView parametersView = new ParametersView()
			{ 
				DataContext = new ParametersViewModel(
					new Entities.Models.DragDropData(), 
					devicesContainer, 
					false) 
			};
			CreateWindow(
				parametersView,
				"Parameters",
				"Parameters",
				DockSide.Right,
				out _parametrs);
			SetCanClose(_parametrs, false);
			SetDesiredWidthInDockedMode(_parametrs, 300);

			PropertyGridView propertiesGridView = new PropertyGridView()
			{ DataContext = propertyGrid };
			CreateWindow(
				propertiesGridView,
				"Properties",
				"Properties",
				DockSide.Bottom,
				out _propertyGrid);
			SetTargetName(_propertyGrid, "Parameters", DockState.Dock);
			SetCanClose(_propertyGrid, false);

			StencilView stencilView = new StencilView()
			{ DataContext = stencil };
			CreateWindow(
				stencilView,
				"Stencil",
				"Stencil",
				DockSide.Left,
				out _stencil);
			SetCanClose(_stencil, false);
			SetDesiredWidthInDockedMode(_stencil, 300);
		}


		#endregion Methods
	}
}
