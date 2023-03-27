using Demo.Services;
using Demo.Utilities;
using GalaSoft.MvvmLight.CommandWpf;
using Solver.Tangram.Game.Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Demo.ViewModel
{
    public class TabClass1 : TabBase, IViewModelExecuteAlgorithmCommand
    {
        private Game _gameInstance;
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private UIGameExecutor _UIGameExecutor;
        private Canvas _canvas;

        public TabClass1(ref Game gameInstance)
        {
            this._gameInstance = gameInstance;
            this._canvas = CreateCanvas();

            this.algorithmDisplayHelper = new AlgorithmDisplayHelper(
                ref this._canvas,
                Dispatcher.CurrentDispatcher);

            this._UIGameExecutor = new UIGameExecutor(
                algorithmDisplayHelper,
                ref _gameInstance);

            ExecuteCommand = new RelayCommand(
                () => ExecuteAlgorithm(),
                this._UIGameExecutor.ExecutorState == UIGameExecutorState.READY);
        }

        public ICommand ExecuteCommand { get; set; }

        public UIElement MyCanvasContent => _canvas;

        public void ExecuteAlgorithm()
        {
            this._UIGameExecutor.ExecuteInBackground();
        }

        private Canvas CreateCanvas()
        {
            return new Canvas()
            {
                Background = new SolidColorBrush()
                {
                    Color = Colors.WhiteSmoke
                },
                Height = 300,
                Width = 600
            };
        }
    }
}
