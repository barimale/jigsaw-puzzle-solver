using Demo.Services;
using Demo.Utilities;
using GalaSoft.MvvmLight.CommandWpf;
using Solver.Tangram.Game.Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Demo.ViewModel.SolverTabs
{
    public class SolutionCircuit : TabBase, IViewModelExecuteAlgorithmCommand
    {
        private Game _gameInstance;
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private UIGameExecutor _UIGameExecutor;
        private Canvas _canvas;

        public SolutionCircuit(ref Game gameInstance)
        {
            _gameInstance = gameInstance;
            _canvas = CreateCanvas();

            algorithmDisplayHelper = new AlgorithmDisplayHelper(
                ref _canvas,
                Dispatcher.CurrentDispatcher);

            _UIGameExecutor = new UIGameExecutor(
                algorithmDisplayHelper,
                ref _gameInstance);

            ExecuteCommand = new RelayCommand(
                () => ExecuteAlgorithm(),
                _UIGameExecutor.ExecutorState == UIGameExecutorState.READY);
        }

        public ICommand ExecuteCommand { get; set; }

        public UIElement MyCanvasContent => _canvas;

        public void ExecuteAlgorithm()
        {
            _UIGameExecutor.ExecuteInBackground();
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
