using System.Windows.Controls;
using System.Windows.Media;

namespace Demo.ViewModel
{
    // game parts: board and blocks
   public class TabClass2 : TabBase
    {
        public string MyStringContent { get; set; }
        public int[] MyNumberCollection { get; set; }
        public int MySelectedNumber { get; set; }
        public object MyCanvasContent { get; set; } = new Canvas()
        {
            Background = new SolidColorBrush()
            {
                Color = Colors.DarkGreen
            }
        };
    }
}
