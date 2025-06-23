
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.Interfaces;
using Dashboard.Models;
using Dashboard.Models.ToolsDesign;
using Dashboard.Services;
using DeviceCommunicators.Models;
using DeviceHandler.ViewModel;
using Newtonsoft.Json;
using Services.Services;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.ViewModels
{
	public class DesignDashboardViewModel: ObservableObject, IDocumentVM
	{
		#region Properties

		public DesignDiagramData DesignDiagram { get; set; }

		public NodeCollection Nodes { get; set; }
		public SnapSettings SnapSettings { get; set; }

		public Syncfusion.UI.Xaml.Diagram.CommandManager CommandManager { get; set; }

		public PageSettings PageSettings { get; set; }

		public object SelectedItems { get; set; }

		public bool IsNeedSave { get; set; }

		public string Name
		{
			get
			{
				if(DesignDiagram == null)
					return null;
				return DesignDiagram.Name;
			}
			set { }
		}

		#endregion Properties

		#region Fields

		private PropertyGridViewModel _propertyGrid;
		private GenerateService _generateService;

		private ResourceDictionary _appResoureces;

		#endregion Fields

		#region Constructor

		public DesignDashboardViewModel(
			string name,
			string filePath,
			PropertyGridViewModel propertyGrid,
			ResourceDictionary appResoureces)
		{
			_appResoureces = appResoureces;

			DesignDiagram = new DesignDiagramData();
			DesignDiagram.Name = name;
			DesignDiagram.FilePath = filePath;

			_propertyGrid = propertyGrid;

			Nodes = new NodeCollection();
			PageSettings = new PageSettings();

			IsNeedSave = false;

			ItemAddedCommand = new RelayCommand<object>(ItemAdded);
			ItemDeletedCommand = new RelayCommand<object>(ItemDeleted);
			Diagram_DropCommand = new RelayCommand<DragEventArgs>(Diagram_Drop);
			ItemSelectedCommand = new RelayCommand<object>(ItemSelected);
			ItemSelectingCommand = new RelayCommand<object>(ItemSelecting);
			SaveDashboradCommand = new RelayCommand(Save);
			GenerateDashboradCommand = new RelayCommand(GenerateDashborad);

			SetSnapAndGrid();

			//AddNodesForDebug();

			ChangeDarkLight();

			_generateService = new GenerateService();
			SelectedItems = new SelectorViewModel();
		}

		#endregion Constructor

		#region Methods

		public bool Dispose()
		{
			if (IsNeedSave)
			{
				MessageBoxResult result = MessageBox.Show(
					$"You've made changes to the dashboard.\r\n" +
					"Do you wisht to save it?",
					"Warning",
					MessageBoxButton.YesNoCancel);
				if (result == MessageBoxResult.Yes)
				{
					Save();
				}
				else if (result == MessageBoxResult.Cancel)
				{
					return false;
				}
			}

			return true;
		}

		private void SetSnapAndGrid()
		{
			SnapSettings = new SnapSettings()
			{
				SnapConstraints = SnapConstraints.ShowLines,
				SnapToObject = SnapToObject.All,
			};
		}

		private void GenerateDashborad()
		{
			_generateService.Generate(DesignDiagram);
		}

		public void Save()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented;
			settings.TypeNameHandling = TypeNameHandling.All;
			var sz = JsonConvert.SerializeObject(DesignDiagram, settings);
			File.WriteAllText(DesignDiagram.FilePath, sz);

			IsNeedSave = false;
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

			IsNeedSave = false;
		}

		#region Add node

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
			if (node == null)
			{
				node = new NodeViewModel();
				node.Content = tool;
				Nodes.Add(node);

				(node.Content as DesignToolBase).PropertyChanged +=  
					DesignTool_PropertyChanged;
			}

			node.ID = toolName;
			node.Pivot = new Point(0, 0);

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
					node.ContentTemplate = _appResoureces["NodeSwitchTemplate"] as DataTemplate;
					break;
				case "ComboBox":
					node.ContentTemplate = _appResoureces["NodeComboBoxTemplate"] as DataTemplate;
					break;
				case "TextBox":
					node.ContentTemplate = _appResoureces["NodeTextBoxTemplate"] as DataTemplate;
					break;
				case "Led":
					node.ContentTemplate = _appResoureces["NodeLedTemplate"] as DataTemplate;
					break;
				case "Gauge":
					node.ContentTemplate = _appResoureces["NodeGaugeTemplate"] as DataTemplate;
					break;
				case "Chart":
					node.ContentTemplate = _appResoureces["NodeChartTemplate"] as DataTemplate;
					break;
				case "Register":
					node.ContentTemplate = _appResoureces["NodeRegisterTemplate"] as DataTemplate;
					break;
				case "MonitorList":
					node.ContentTemplate = _appResoureces["NodeMonitorListTemplate"] as DataTemplate;
					break;
				case "CommandsList":
					node.ContentTemplate = _appResoureces["NodeCommandsListTemplate"] as DataTemplate;
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
					node.UnitWidth = 270;
					node.UnitHeight = 40;
					break;
				case "ComboBox":
					node.Content = new DesignToolComboBox();
					node.UnitWidth = 250;
					node.UnitHeight = 40;
					break;
				case "TextBox":
					node.Content = new DesignToolTextBox();
					node.UnitWidth = 250;
					node.UnitHeight = 40;
					break;
				case "Led":
					node.Content = new DesignToolLed();
					node.UnitWidth = 200;
					node.UnitHeight = 40;
					break;
				case "Gauge":
					node.Content = new DesignToolGauge();
					node.UnitWidth = 100;
					node.UnitHeight = 150;
					break;
				case "Chart":
					node.Content = new DesignToolChart();
					node.UnitWidth = 150;
					node.UnitHeight = 150;
					break;
				case "Register":
					node.Content = new DesignToolRegister();
					node.UnitWidth = 350;
					node.UnitHeight = 200;
					break;
				case "MonitorList":
					node.Content = new DesignToolMonitorList();
					node.UnitWidth = 420;
					node.UnitHeight = 100;
					break;
				case "CommandsList":
					node.Content = new DesignToolCommandsList();
					node.UnitWidth = 500;
					node.UnitHeight = 100;
					break;
			}

			(node.Content as DesignToolBase).PropertyChanged += 
				DesignTool_PropertyChanged;
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

		#endregion Add node

		private void ItemDeleted(object item)
		{
			if (!(item is ItemDeletedEventArgs itemDeleted))
				return;

			if (!(itemDeleted.Item is NodeViewModel node))
				return;

			DesignDiagram.ToolList.Remove(node.Content as DesignToolBase);

			IsNeedSave = true;
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



		private void DesignTool_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IsNeedSave = true;
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

			toolBase.SetParameter(param);
			SetPropertyGridSelectedNode(toolBase);
		}

		private void ItemSelected(object e)
		{
			if (!(e is ItemSelectedEventArgs itemSelectedData))
				return;

			if (!(itemSelectedData.Item is NodeViewModel node))
				return;

			if (!(node.Content is DesignToolBase toolBase))
				return;

			SetPropertyGridSelectedNode(toolBase);
		}

		private void ItemSelecting(object e)
		{
			//SelectorViewModel svm = (SelectedItems as SelectorViewModel);
			//svm.SelectorConstraints =
			//	svm.SelectorConstraints & ~SelectorConstraints.QuickCommands;
			//(svm.Commands as QuickCommandCollection).RemoveAt(1);
		}

		private void SetPropertyGridSelectedNode(DesignToolBase toolBase)
		{
			_propertyGrid.SetHideProperties(toolBase);
			_propertyGrid.SelectedNode = toolBase;
		}

		public void ChangeDarkLight()
		{
			PageSettings.PageBackground =
				_appResoureces["MahApps.Brushes.ThemeBackground"] as SolidColorBrush;
		}

		#endregion Methods

		#region Commands

		public RelayCommand<object> ItemAddedCommand { get; private set; }
		public RelayCommand<object> ItemDeletedCommand { get; private set; }
		public RelayCommand<DragEventArgs> Diagram_DropCommand { get; private set; }
		public RelayCommand<object> ItemSelectedCommand { get; private set; }
		public RelayCommand<object> ItemSelectingCommand { get; private set; }


		public RelayCommand SaveDashboradCommand { get; private set; }
		public RelayCommand GenerateDashboradCommand { get; private set; }

		public RelayCommand CopyCommand { get; private set; }
		public RelayCommand PastCommand { get; private set; }
		public RelayCommand SaveCommand { get; private set; }

		#endregion Commands
	}
}
