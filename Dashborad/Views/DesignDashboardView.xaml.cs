using Controls.Interfaces;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Views
{
	/// <summary>
	/// Interaction logic for DesignDashboardView.xaml
	/// </summary>
	public partial class DesignDashboardView : UserControl, IDocumentV
	{
		public DesignDashboardView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			SelectorViewModel svm = (diagram.SelectedItems as SelectorViewModel);
			(svm.Commands as QuickCommandCollection).RemoveAt(1);
			svm.SelectorConstraints =
				svm.SelectorConstraints & ~SelectorConstraints.Rotator;
		}
	}
}
