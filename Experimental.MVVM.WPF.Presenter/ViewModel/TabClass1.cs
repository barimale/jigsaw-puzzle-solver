using Solver.Tangram.Game.Logic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo.ViewModel
{
    public class TabClass1 : TabBase
    {
        private Game _gameInstance;

        public TabClass1(ref Game gameInstance)
        {
            this._gameInstance = gameInstance;
        }

        // expose data as properties

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
