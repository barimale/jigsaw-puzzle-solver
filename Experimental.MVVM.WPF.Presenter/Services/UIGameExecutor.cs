using Demo.Utilities;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.Game.Logic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class UIGameExecutor
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private Game konfiguracjaGry;

        public event EventHandler<SourceEventArgs> TerminationReached;
        public event EventHandler<SourceEventArgs> AlgorithmRanStatus;

        private Task ExecuteInBackgroundTask;
        public UIGameExecutorState ExecutorState => ObtainExecutorState();

        public UIGameExecutor(
            AlgorithmDisplayHelper algorithmDisplayHelper,
            ref Game dataInput)
        {
            this.algorithmDisplayHelper = algorithmDisplayHelper;
            konfiguracjaGry = dataInput;

            if (konfiguracjaGry.Algorithm == null && konfiguracjaGry.Multialgorithm == null)
                throw new Exception("At least one algorithm has to be set");

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
            algorithmDisplayHelper.Algorithm_Ran(sender, e);
        }

        public void ExecuteInBackground(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (ExecutorState == UIGameExecutorState.READY)
                {
                    algorithmDisplayHelper.Reset();
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
                algorithmDisplayHelper.Algorithm_TerminationReached(result, null);
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
