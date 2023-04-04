using Demo.Services;
using GalaSoft.MvvmLight.CommandWpf;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.Game.Logic;
using System.Collections.Generic;
using System.Threading;
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
        private UIGameExecutor _UIGameExecutor;
        private CancellationTokenSource _cts;
        private Dictionary<string, Canvas> _canvases = new Dictionary<string, Canvas>();

        public SolutionCircuit(ref Game gameInstance)
        {
            _gameInstance = gameInstance;
            _cts = new CancellationTokenSource();

            // canvases
            if (_gameInstance.HasManyAlgorithms)
            {
                foreach(var algorithm in _gameInstance.Multialgorithm.Algorithms)
                {
                    var canvas = CreateCanvas(algorithm.Key);
                    _canvases.TryAdd(algorithm.Key, canvas);
                }
            }else
            {
                var algorithmKey = _gameInstance.Algorithm.Id;
                var canvas = CreateCanvas(algorithmKey);
                _canvases.TryAdd(algorithmKey, canvas);
            }

            // helpers based on canvases
            _UIGameExecutor = new UIGameExecutor(
                ref _gameInstance,
                Dispatcher.CurrentDispatcher,
                _canvases);

            _UIGameExecutor.AlgorithmRanStatus += _UIGameExecutor_AlgorithmRanStatus;

            // commands
            ExecuteCommand = new RelayCommand(
                () => ExecuteAlgorithm(),
                _UIGameExecutor.ExecutorState == UIGameExecutorState.READY);
            CancellCommand = new RelayCommand(
                () => CancellAlgorithm(),
                _UIGameExecutor.ExecutorState == UIGameExecutorState.ACTIVATED);

            // initial state
            IsExecutable = true;
            IsCancellable = false;
        }

        private bool _isExecutable;
        public bool IsExecutable
        {
            get => _isExecutable;
            set
            {
                if (_isExecutable != value)
                {
                    Set(() => IsExecutable, ref _isExecutable, value);
                }
            }
        }

        private bool _isCancellable;
        public bool IsCancellable
        {
            get => _isCancellable;
            set
            {
                if (_isCancellable != value)
                {
                    Set(() => IsCancellable, ref _isCancellable, value);
                }
            }
        }

        private void _UIGameExecutor_AlgorithmRanStatus(object sender, SourceEventArgs e)
        {
            var status = sender as string;
            UIGameExecutorStatus = status;
            IsExecutable = _UIGameExecutor.ExecutorState == UIGameExecutorState.READY;
            IsCancellable = _UIGameExecutor.ExecutorState == UIGameExecutorState.ACTIVATED;
        }

        public ICommand ExecuteCommand { get; set; }

        public ICommand BreakContinueCommand { get; set; }

        public ICommand CancellCommand { get; set; }

        public UIElement MyCanvasContent => ToTabControl(); // _canvas;

        private UIElement ToTabControl()
        {
            var tabControl = new TabControl();
            tabControl.Margin = new Thickness(10, 10, 10, 10);
            foreach (var canvas in _canvases)
            {
                var tab = new TabItem();
                var scrooller = new ScrollViewer();
                scrooller.Content = canvas.Value;
                tab.Content = scrooller;
                tab.Tag = canvas.Key;
                tab.Header = _gameInstance.GetAlgorithmNameBy(canvas.Key);
                tab.FontSize = 10;
                tab.ToolTip = _gameInstance.GetAlgorithmNameBy(canvas.Key);
                // TODO datatemplate here max width
                // do the header color green on termination reached
                tabControl.Items.Add(tab);
            }

            return tabControl;
        }

        private string _UIGameExecutorStatus;
        public string UIGameExecutorStatus
        {
            get => _UIGameExecutorStatus;
            set
            {
                if (_UIGameExecutorStatus != value)
                {
                    Set(() => UIGameExecutorStatus, ref _UIGameExecutorStatus, value);
                }
            }
        }

        public void ExecuteAlgorithm()
        {
            _cts.TryReset();
            _UIGameExecutor.ExecuteInBackground(_cts.Token);
        }

        public void CancellAlgorithm()
        {
            _cts.Cancel();
        }

        private Canvas CreateCanvas(string id)
        {
            return new Canvas()
            {
                Background = new SolidColorBrush()
                {
                    Color = Colors.WhiteSmoke
                },
                Margin = new Thickness(6, 6, 6, 6),
                Height = 300,
                Width = 600,
                Tag = id
            };
        }
    }
}
