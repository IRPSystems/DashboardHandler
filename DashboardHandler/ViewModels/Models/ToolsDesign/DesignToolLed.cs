
using System.Windows.Media;

namespace DashboardHandler.ViewModels.Models.ToolsDesign
{
    public class DesignToolLed : DesignToolBase
    {
        public bool IsChecked { get; set; }
        public Brush OnColor { get; set; }
        public Brush OffColor { get; set; }
    }
}
