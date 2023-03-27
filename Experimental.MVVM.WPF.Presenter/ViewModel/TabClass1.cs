using System.Windows.Controls;
using System.Windows.Media;

namespace Demo.ViewModel
{
    public class TabClass1 : TabBase
    {
        public string MyStringContent { get; set; }
        public string MyStringContent2 { get; set; }
        public object MyCanvasContent { get; set; } = new Canvas()
        {
            Background = new SolidColorBrush()
            {
                Color = Colors.Chocolate
            }
        };
    }
}
