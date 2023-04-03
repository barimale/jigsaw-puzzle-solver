using Demo.Utilities;
using Experimental.MVVM.WPF.Solver.Presenter.Utilities;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Demo.Services
{
    public class UIGameExecutor
    {
        //private DisplayerMultiton _displayerMultiton;

        private Dictionary<string, AlgorithmDisplayHelper> algorithmDisplayHelpers = new Dictionary<string, AlgorithmDisplayHelper>();
        private Game konfiguracjaGry;

        public event EventHandler<SourceEventArgs> TerminationReached;
        public event EventHandler<SourceEventArgs> AlgorithmRanStatus;

        private Task ExecuteInBackgroundTask;
        public UIGameExecutorState ExecutorState => ObtainExecutorState();

        public UIGameExecutor(
            ref Game dataInput,
            Dispatcher dispatcher,
            Dictionary<string, Canvas> keyedCanvases)
        {
            foreach(var canvas in keyedCanvases)
            {
                algorithmDisplayHelpers.TryAdd(
                    canvas.Key,
                    new AlgorithmDisplayHelper(
                        canvas.Value,
                        dispatcher));
            }

            konfiguracjaGry = dataInput;

            if (konfiguracjaGry.Algorithm == null && konfiguracjaGry.Multialgorithm == null)
                throw new Exception("At least one algorithm has to be set");

            // CONTINUE FROM HERE
            //this._displayerMultiton = new DisplayerMultiton();
            //konfiguracjaGry.Multialgorithm.Algorithms.Keys.ToList().ForEach(p =>
            //{
            //    this._displayerMultiton[p] = algorithmDisplayHelpers[p];
            //});

            if (konfiguracjaGry.Algorithm != null)
            {
                konfiguracjaGry.Algorithm.QualityCallback += Algorithm_QualityCallback;
                konfiguracjaGry.Algorithm.QualityCallback += HandleAlgorithmRanStatus;
            }

            if (konfiguracjaGry.Multialgorithm != null)
            {
                konfiguracjaGry.Multialgorithm.QualityCallback += Algorithm_QualityCallback;
                konfiguracjaGry.Multialgorithm.QualityCallback += HandleAlgorithmRanStatus;
            }
        }

        private void HandleAlgorithmTerminationStatus(object sender, SourceEventArgs e)
        {
            ExecuteInBackgroundTask = null;
            HandleAlgorithmRanStatus(sender, e);
        }

        private void HandleAlgorithmRanStatus(object sender, SourceEventArgs e)
        {
            ObtainExecutorState();
            var result = sender as AlgorithmResult;
            var sourceName = GetSourceName(result);

            AlgorithmRanStatus?.Invoke(
                this.ExecutorState.ToString(),
                new SourceEventArgs()
                {
                    SourceName = sourceName
                });
        }

        private string? GetSourceName(AlgorithmResult result)
        {
            var unknown = "unknown";

            try
            {
                if (result == null)
                    return unknown;

                var name = result
                    .Solution
                    .GetType()
                    .Name
                    .ToString();

                return name;
            }
            catch (Exception)
            {
                return unknown;
            }
        }

        private void Algorithm_QualityCallback(object sender, SourceEventArgs e)
        {
            algorithmDisplayHelpers[e.SourceId].Algorithm_Ran(sender, e);
        }

        public void ExecuteInBackground(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (ExecutorState == UIGameExecutorState.READY)
                {
                    algorithmDisplayHelpers.Values.ToList().ForEach(p => p.Reset());
                    ExecuteInBackgroundTask = Task.Factory.StartNew(() => DoExecuteAsync(), ct);
                }
            }
            catch (OperationCanceledException oce)
            {
                ExecuteInBackgroundTask.Dispose();
                throw oce;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DoExecuteAsync()
        {
            try
            {
                var result = await konfiguracjaGry.RunGameAsync<AlgorithmResult>();

                HandleAlgorithmTerminationStatus(this, null);
                Termination_Reached(result, null);
                algorithmDisplayHelpers
                    .Keys
                    .ToList()
                    .ForEach(p => algorithmDisplayHelpers[p].Algorithm_TerminationReached(
                        result,
                        new SourceEventArgs() // It needs to be corrected
                        {
                            SourceName = p,
                            SourceId = p
                        }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private UIGameExecutorState ObtainExecutorState()
        {
            if (ExecuteInBackgroundTask == null)
            {
                return UIGameExecutorState.READY;
            }

            if (ExecuteInBackgroundTask != null && (
                ExecuteInBackgroundTask.Status == TaskStatus.RanToCompletion
                || ExecuteInBackgroundTask.Status == TaskStatus.Running))
            {
                return UIGameExecutorState.ACTIVATED;
            }
            else
            {
                return UIGameExecutorState.UNKNOWN;
            }
        }

        private void Termination_Reached(object sender, SourceEventArgs e)
        {
            TerminationReached?.Invoke(sender, e);
        }
    }
}
