﻿
using CommunityToolkit.Mvvm.ComponentModel;
using Dashboard.Models.ToolsDesign;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Dashboard.ViewModels
{
    public class PropertyGridViewModel: ObservableObject
    {
		#region Properties

		public DesignToolBase SelectedNode { get; set; }

		public ObservableCollection<string> HidePropertiesList { get; set; }

		public Brush Background { get; set; }
		public Brush Foreround { get; set; }

		#endregion Properties

		#region Fields

		private ResourceDictionary _appResoureces;

		#endregion Fields

		#region Constructor

		public PropertyGridViewModel(
			ResourceDictionary appResoureces) 
		{
			_appResoureces = appResoureces;

			HidePropertiesList = new ObservableCollection<string>();
		}

		#endregion Constructor

		#region Methods

		public void SetHideProperties(DesignToolBase tool)
		{ 
			HidePropertiesList.Clear();
			List<string> hideProperties = tool.GetHideProperties();
			foreach (string property in hideProperties)
				HidePropertiesList.Add(property);

			OnPropertyChanged(nameof(HidePropertiesList));
		}

		public void ChangeDarkLight()
		{
			Background =
				_appResoureces["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
			Foreround =
				_appResoureces["MahApps.Brushes.ThemeForeground"] as SolidColorBrush;
		}

		#endregion Methods

		#region Commands


		#endregion Commands
	}
}
