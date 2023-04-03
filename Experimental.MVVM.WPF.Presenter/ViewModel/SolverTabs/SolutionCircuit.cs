using Demo.Services;
using GalaSoft.MvvmLight.CommandWpf;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.Game.Logic;
using System;
using System.Collections.Generic;
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
        private Dictionary<string, Canvas> _canvases = new Dictionary<string, Canvas>();

        public SolutionCircuit(ref Game gameInstance)
        {
            _gameInstance = gameInstance;

            // canvases
            if(_gameInstance.HasManyAlgorithms)
            {
                foreach(var algorithm in _gameInstance.Multialgorithm.Algorithms)
                {
                    var canvas = CreateCanvas(algorithm.Key);
                    _canvases.TryAdd(algorithm.Key, canvas);
                }
            }else
            {
                var algorithmKey = Guid.NewGuid().ToString();
                var canvas = CreateCanvas(algorithmKey);
                _canvases.TryAdd(algorithmKey, canvas);
            }

            // helpers based on canvases
            _UIGameExecutor = new UIGameExecutor(
                ref _gameInstance,
                Dispatcher.CurrentDispatcher,
                _canvases);

            _UIGameExecutor.AlgorithmRanStatus += _UIGameExecutor_AlgorithmRanStatus;

            ExecuteCommand = new RelayCommand(
                () => ExecuteAlgorithm(),
                _UIGameExecutor.ExecutorState == UIGameExecutorState.READY);
        }

        private void _UIGameExecutor_AlgorithmRanStatus(object sender, SourceEventArgs e)
        {
            var status = sender as string;
            UIGameExecutorStatus = status;
            // TODO detect why this is pass to the event
            if(e != null && e.SourceName != "unknown")
            {
                AlgorithmResultSource = e.SourceName;
            }
        }

        public ICommand ExecuteCommand { get; set; }

        public UIElement MyCanvasContent => ToTabControl(); // _canvas;

        private UIElement ToTabControl()
        {
            var tabControl = new TabControl();
            foreach(var canvas in _canvases)
            {
                var tab = new TabItem();
                tab.Content = canvas.Value;
                tab.Tag = canvas.Key;
                tabControl.Items.Add(tab);
            }

            return tabControl;
        }

        private string _algorithmResultSource;
        public string AlgorithmResultSource
        {
            get => _algorithmResultSource;
            set
            {
                if (_algorithmResultSource != value)
                {
                    Set(() => AlgorithmResultSource, ref _algorithmResultSource, value);
                }
            }
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
            _UIGameExecutor.ExecuteInBackground();
        }

        private Canvas CreateCanvas(string id)
        {
            return new Canvas()
            {
                Background = new SolidColorBrush()
                {
                    Color = Colors.WhiteSmoke
                },
                Height = 300,
                Width = 600,
                Tag = id
            };
        }
    }
}
