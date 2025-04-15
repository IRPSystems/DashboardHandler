
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardHandler.Models.ToolsDesign;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
	public class DesignDashboardViewModel: ObservableObject
	{
		#region Properties

		public SfDiagram Diagram { get; set; }
		public string Name { get; set; }

		//public NodeCollection Nodes { get; set; }
		//public SnapSettings SnapSettings { get; set; }

		public Syncfusion.UI.Xaml.Diagram.CommandManager CommandManager { get; set; }

		#endregion Properties

		#region Constructor

		public DesignDashboardViewModel(string name)
		{
			Name = name;

			Diagram = new SfDiagram();
			Diagram.Nodes = new NodeCollection();

			SetSnapAndGrid();

			AddNodesForDebug();
		}

		#endregion Constructor

		#region Methods

		private void SetSnapAndGrid()
		{
			Diagram.SnapSettings = new SnapSettings()
			{
				SnapConstraints = SnapConstraints.ShowLines,
				SnapToObject = SnapToObject.All,
			};
		}

		private void AddNodesForDebug()
		{
			DesignToolBase designTool = new DesignToolSwitch()
			{
				IsChecked = true,
				OnDescription = "ON",
				OffDescription = "OFF",
			};

			NodeViewModel node = new NodeViewModel()
			{
				OffsetX = 100,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "Switch",

				ContentTemplate = Application.Current.Resources["NodeSwitchTemplate"] as DataTemplate
			};

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolComboBox()
			{
				Items = new ObservableCollection<object> { "Item 1", "Item 2", "Item 3", }
			};
			(designTool as DesignToolComboBox).SelectedItem =
				(designTool as DesignToolComboBox).Items[1];

			node = new NodeViewModel()
			{
				OffsetX = 200,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "ComboBox",

				ContentTemplate = Application.Current.Resources["NodeComboBoxTemplate"] as DataTemplate
			};

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolTextBox()
			{
				Text = "The Text!!",
			};

			node = new NodeViewModel()
			{
				OffsetX = 300,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "TextBox",

				ContentTemplate = Application.Current.Resources["NodeTextBoxTemplate"] as DataTemplate
			};

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolLed()
			{
				IsChecked = false,
				OffColor = Brushes.Red,
				OnColor = Brushes.Green,
			};

			node = new NodeViewModel()
			{
				UnitHeight = 30,
				UnitWidth = 30,

				OffsetX = 400,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "Led",

				ContentTemplate = Application.Current.Resources["NodeLedTemplate"] as DataTemplate
			};

			designTool.ParentNode = node;

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolGauge()
			{
				
			};

			node = new NodeViewModel()
			{
				UnitHeight = 30,
				UnitWidth = 30,

				OffsetX = 500,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "Gauge",

				ContentTemplate = Application.Current.Resources["NodeGaugeTemplate"] as DataTemplate
			};

			designTool.ParentNode = node;

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolChart()
			{

			};

			node = new NodeViewModel()
			{
				UnitHeight = 30,
				UnitWidth = 30,

				OffsetX = 600,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "Chart",

				ContentTemplate = Application.Current.Resources["NodeChartTemplate"] as DataTemplate
			};

			designTool.ParentNode = node;

			(Diagram.Nodes as NodeCollection).Add(node);

			///////////////////////////////////////////////////////////

			designTool = new DesignToolRegister()
			{

			};

			node = new NodeViewModel()
			{
				UnitWidth = 70,

				OffsetX = 700,
				OffsetY = 100,
				Pivot = new Point(0, 0),

				Content = designTool,
				ID = "Register",

				ContentTemplate = Application.Current.Resources["NodeRegisterTemplate"] as DataTemplate
			};

			designTool.ParentNode = node;

			(Diagram.Nodes as NodeCollection).Add(node);
		}

		

		#endregion Methods

		#region Commands

		public RelayCommand SaveCommand { get; private set; }
		public RelayCommand CopyCommand { get; private set; }
		public RelayCommand DuplicateCommand { get; private set; }

		#endregion Commands
	}
}
