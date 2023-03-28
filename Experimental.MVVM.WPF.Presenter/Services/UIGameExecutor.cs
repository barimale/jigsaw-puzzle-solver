using Demo.Utilities;
using Solver.Tangram.AlgorithmDefinitions.Generics;
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

        public event EventHandler TerminationReached;
        public event EventHandler AlgorithmRanStatus;

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
                konfiguracjaGry.Algorithm.QualityCallback += HandleAlgorithmRanStatus; ;
            }

            if (konfiguracjaGry.Multialgorithm != null)
            {
                konfiguracjaGry.Algorithm.QualityCallback += Algorithm_QualityCallback;
                konfiguracjaGry.Algorithm.QualityCallback += HandleAlgorithmRanStatus;
            }
        }

        private void HandleAlgorithmRanStatus(object sender, EventArgs e)
        {
            ObtainExecutorState();
            AlgorithmRanStatus?.Invoke(this.ExecutorState.ToString(), e);
        }

        private void Algorithm_QualityCallback(object sender, EventArgs e)
        {
            algorithmDisplayHelper.Algorithm_Ran(sender, e);
        }

        public void ExecuteInBackground(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (ExecutorState == UIGameExecutorState.READY)
                {
                    ExecuteInBackgroundTask = Task.Factory.StartNew(async () => await DoExecuteAsync(), ct);
                    //ExecuteInBackgroundTask.Start();
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

            if (ExecuteInBackgroundTask != null && ExecuteInBackgroundTask.Status != TaskStatus.Running)
            {
                return UIGameExecutorState.READY;
            }
            else
            {
                return UIGameExecutorState.ACTIVATED;
            }
        }

        private void Termination_Reached(object sender, EventArgs e)
        {
            TerminationReached?.Invoke(sender, e);
        }
    }
}
