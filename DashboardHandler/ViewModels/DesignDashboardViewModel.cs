
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using DashboardHandler.Models;
using DashboardHandler.Models.ToolsDesign;
using DeviceCommunicators.Models;
using DeviceHandler.ViewModel;
using Newtonsoft.Json;
using Services.Services;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DashboardHandler.ViewModels
{
	public class DesignDashboardViewModel: ObservableObject
	{
		#region Properties

		public DesignDiagramData DesignDiagram { get; set; }

		public NodeCollection Nodes { get; set; }
		public SnapSettings SnapSettings { get; set; }

		public Syncfusion.UI.Xaml.Diagram.CommandManager CommandManager { get; set; }

		public PageSettings PageSettings { get; set; }

		#endregion Properties

		#region Fields

		private PropertyGridViewModel _propertyGrid;
		

		#endregion Fields

		#region Constructor

		public DesignDashboardViewModel(
			string name,
			string filePath,
			PropertyGridViewModel propertyGrid)
		{
			DesignDiagram = new DesignDiagramData();
			DesignDiagram.Name = name;
			DesignDiagram.FilePath = filePath;

			_propertyGrid = propertyGrid;

			Nodes = new NodeCollection();
			PageSettings = new PageSettings();

			ItemAddedCommand = new RelayCommand<object>(ItemAdded);
			Diagram_DropCommand = new RelayCommand<DragEventArgs>(Diagram_Drop);
			ItemSelectedCommand = new RelayCommand<object>(ItemSelected);

			SetSnapAndGrid();

			//AddNodesForDebug();

			ChangeDarkLight();
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

		public void Save()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.TypeNameHandling = TypeNameHandling.All;
			var sz = JsonConvert.SerializeObject(DesignDiagram, settings);
			File.WriteAllText(DesignDiagram.FilePath, sz);
		}

		public void Open(string path)
		{
			string jsonString = File.ReadAllText(path);

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.TypeNameHandling = TypeNameHandling.All;
			DesignDiagram = JsonConvert.DeserializeObject(jsonString, settings) as DesignDiagramData;

			foreach (DesignToolBase tool in DesignDiagram.ToolList)
			{
				string toolName = tool.GetType().Name;
				toolName = toolName.Replace("DesignTool", string.Empty);
				
				InitNodeBySymbol(null, toolName, tool);
			}
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
			string toolName,
			DesignToolBase tool = null)
		{
			if(node == null)
			{
				node = new NodeViewModel();
				Nodes.Add(node);
			}

			node.ID = toolName;

			SetNewNodeTemplate(node, toolName);

			if (tool == null)
			{
				SetNewNodeContentAndSize(node, toolName);
			}

			MatchNewNodeToTool(node, tool);

			node.PropertyChanged += Node_PropertyChanged;

			if (tool == null)
				DesignDiagram.ToolList.Add(node.Content as DesignToolBase);
		}

		private void SetNewNodeTemplate(
			NodeViewModel node, 
			string toolName)
		{
			switch (toolName)
			{
				case "Switch":
					node.ContentTemplate = App.Current.Resources["NodeSwitchTemplate"] as DataTemplate;
					break;
				case "ComboBox":
					node.ContentTemplate = App.Current.Resources["NodeComboBoxTemplate"] as DataTemplate;
					break;
				case "TextBox":
					node.ContentTemplate = App.Current.Resources["NodeTextBoxTemplate"] as DataTemplate;
					break;
				case "Led":
					node.ContentTemplate = App.Current.Resources["NodeLedTemplate"] as DataTemplate;
					break;
				case "Gauge":
					node.ContentTemplate = App.Current.Resources["NodeGaugeTemplate"] as DataTemplate;
					break;
				case "Chart":
					node.ContentTemplate = App.Current.Resources["NodeChartTemplate"] as DataTemplate;
					break;
				case "Register":
					node.ContentTemplate = App.Current.Resources["NodeRegisterTemplate"] as DataTemplate;
					break;
				case "MonitorList":
					node.ContentTemplate = App.Current.Resources["NodeMonitorListTemplate"] as DataTemplate;
					break;
				case "CommandsList":
					node.ContentTemplate = App.Current.Resources["NodeCommandsListTemplate"] as DataTemplate;
					break;
			}
		}

		private void SetNewNodeContentAndSize(
			NodeViewModel node,
			string toolName)
		{
			switch (toolName)
			{
				case "Switch":
					node.Content = new DesignToolSwitch();
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "ComboBox":
					node.Content = new DesignToolComboBox();
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "TextBox":
					node.Content = new DesignToolTextBox();
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "Led":
					node.Content = new DesignToolLed();
					node.UnitWidth = 40;
					node.UnitHeight = 40;
					break;
				case "Gauge":
					node.Content = new DesignToolGauge();
					node.UnitWidth = 100;
					node.UnitHeight = 100;
					break;
				case "Chart":
					node.Content = new DesignToolChart();
					node.UnitWidth = 100;
					node.UnitHeight = 100;
					break;
				case "Register":
					node.Content = new DesignToolRegister();
					node.UnitWidth = 100;
					node.UnitHeight = 40;
					break;
				case "MonitorList":
					node.Content = new DesignToolMonitorList();
					node.UnitWidth = 300;
					node.UnitHeight = 100;
					break;
				case "CommandsList":
					node.Content = new DesignToolCommandsList();
					node.UnitWidth = 300;
					node.UnitHeight = 100;
					break;
			}
		}

		private void MatchNewNodeToTool(
			NodeViewModel node,
			DesignToolBase tool)
		{
			if (node == null)
				return;

			if (tool != null)
			{
				node.OffsetX = tool.OffsetX;
				node.OffsetY = tool.OffsetY;
				node.UnitWidth = tool.Width;
				node.UnitHeight = tool.Height;
			}

			else if (node.Content is DesignToolBase toolNew)
			{
				toolNew.OffsetX = node.OffsetX;
				toolNew.OffsetY = node.OffsetY;
				toolNew.Width = node.UnitWidth;
				toolNew.Height = node.UnitHeight;
			}
		}

		private void Node_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(!(sender is NodeViewModel node))
				return;

			if (!(node.Content is DesignToolBase tool))
				return;

			if (e.PropertyName == "OffsetX")
			{
				tool.OffsetX = node.OffsetX;
			}

			if (e.PropertyName == "OffsetY")
			{
				tool.OffsetY = node.OffsetY;
			}

			if (e.PropertyName == "UnitWidth")
			{
				tool.Width = node.UnitWidth;
			}

			if (e.PropertyName == "UnitHeight")
			{
				tool.Height = node.UnitHeight;
			}
		}

		private void Diagram_Drop(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(ParametersViewModel.DragDropFormat) == false)
				return;

			DeviceParameterData param = null;
			var dragData = e.Data.GetData(ParametersViewModel.DragDropFormat);
			if (dragData is DeviceParameterData)
				param = (DeviceParameterData)dragData;
			else if (dragData is System.Collections.IList list)
			{
				var enumerator = list.GetEnumerator();
				enumerator.MoveNext();
				param = (DeviceParameterData)enumerator.Current;
			}

			if (param == null) 
				return;

			Node node =
				FindAncestorService.FindAncestor<Node>((DependencyObject)e.OriginalSource);
			if (node == null || !(node.Content is DesignToolBase toolBase))
				return;

			toolBase.ParameterData = param;
			_propertyGrid.SelectedNode = toolBase;
		}

		private void ItemSelected(object e)
		{
			if (!(e is ItemSelectedEventArgs itemSelectedData))
				return;

			if (!(itemSelectedData.Item is NodeViewModel node))
				return;

			if (!(node.Content is DesignToolBase toolBase))
				return;

			_propertyGrid.SelectedNode = toolBase;
		}

		public void ChangeDarkLight()
		{
			PageSettings.PageBackground =
				App.Current.Resources["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
		}

		#endregion Methods

		#region Commands

		public RelayCommand<object> ItemAddedCommand { get; private set; }
		public RelayCommand<DragEventArgs> Diagram_DropCommand { get; private set; }
		public RelayCommand<object> ItemSelectedCommand { get; private set; }

		#endregion Commands
	}
}
