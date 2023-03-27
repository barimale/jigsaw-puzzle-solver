using Solver.Tangram.Game.Logic;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo.ViewModel
{
    // game parts: board and blocks
   public class TabClass2 : TabBase
    {
        private Game _gameInstance;

        public TabClass2(ref Game gameInstance)
        {
            this._gameInstance = gameInstance;
        }

        public string MyStringContent { get; set; }
        public int[] MyNumberCollection { get; set; }
        public int MySelectedNumber { get; set; }
        public object MyCanvasContent { get; set; } = new TabControl()
        {
            Background = new SolidColorBrush()
            {
                Color = Colors.DarkGreen,
            },
            ItemsSource = new List<TabItem>()
            {
                new TabItem()
                {
                    Content = "DUPA 3",
                    Header = "DUPA",
                },
                new TabItem()
                {
                    Content = "DUPA 4",
                    Header = "DUPA2"
                }
            }
        };
    }
}
