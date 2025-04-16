
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashboardHandler.Models.ToolsDesign;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
	public class DesignDashboardViewModel: ObservableObject
	{
		#region Properties

		public string Name { get; set; }

		public NodeCollection Nodes { get; set; }
		public SnapSettings SnapSettings { get; set; }

		public Syncfusion.UI.Xaml.Diagram.CommandManager CommandManager { get; set; }

		#endregion Properties

		#region Constructor

		public DesignDashboardViewModel(string name)
		{
			Name = name;

			Nodes = new NodeCollection();

			ItemAddedCommand = new RelayCommand<object>(ItemAdded);

			SetSnapAndGrid();

			//AddNodesForDebug();
		}

		#endregion Constructor

		#region Methods

		private void SetSnapAndGrid()
		{
			SnapSettings = new SnapSettings()
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

			Nodes.Add(node);

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

			Nodes.Add(node);

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

			Nodes.Add(node);

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

			Nodes.Add(node);

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

			Nodes.Add(node);

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

			Nodes.Add(node);

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

			Nodes.Add(node);
		}

		private void ItemAdded(object item)
		{
			if (!(item is ItemAddedEventArgs itemAdded))
				return;

			if (!(itemAdded.Item is NodeViewModel node))
				return;

			if (itemAdded.OriginalSource is SymbolViewModel symbol)
			{
				InitNodeBySymbol(node, symbol.Symbol as string);
			}
			else
			{
				InitNodeBySymbol(node, (itemAdded.Info as PasteCommandInfo).SourceId as string);
			}
		}

		private void InitNodeBySymbol(
			NodeViewModel node,
			string toolName)
		{

			node.ID = toolName;

			switch(toolName)
			{
				case "Switch":
					node.Content = new DesignToolSwitch();
					node.ContentTemplate = App.Current.Resources["NodeSwitchTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "ComboBox":
					node.Content = new DesignToolComboBox();
					node.ContentTemplate = App.Current.Resources["NodeComboBoxTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "TextBox":
					node.Content = new DesignToolTextBox();
					node.ContentTemplate = App.Current.Resources["NodeTextBoxTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "Led":
					node.Content = new DesignToolLed();
					node.ContentTemplate = App.Current.Resources["NodeLedTemplate"] as DataTemplate;
					node.UnitWidth = 30;
					node.UnitHeight = 30;
					break;
				case "Gauge":
					node.Content = new DesignToolGauge();
					node.ContentTemplate = App.Current.Resources["NodeGaugeTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 100;
					break;
				case "Chart":
					node.Content = new DesignToolChart();
					node.ContentTemplate = App.Current.Resources["NodeChartTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 100;
					break;
				case "Register":
					node.Content = new DesignToolRegister();
					node.ContentTemplate = App.Current.Resources["NodeRegisterTemplate"] as DataTemplate;
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "MonitorList":
					node.Content = new DesignToolMonitorList();
					node.ContentTemplate = App.Current.Resources["NodeMonitorListTemplate"] as DataTemplate;
					node.UnitWidth = 300;
					node.UnitHeight = 100;
					break;
				case "CommandsList":
					node.Content = new DesignToolCommandsList();
					node.ContentTemplate = App.Current.Resources["NodeCommandsListTemplate"] as DataTemplate;
					node.UnitWidth = 300;
					node.UnitHeight = 100;
					break;
			}
		}

		#endregion Methods

		#region Commands

		public RelayCommand<object> ItemAddedCommand { get; private set; }

		#endregion Commands
	}
}
