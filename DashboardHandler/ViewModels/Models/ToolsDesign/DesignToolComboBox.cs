
using System.Collections.ObjectModel;

namespace DashboardHandler.ViewModels.Models.ToolsDesign
{
    public class DesignToolComboBox : DesignToolBase
    {
        public ObservableCollection<object> Items { get; set; }
        public object SelectedItem { get; set; }
    }
}
