using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.Game.Logic;
using System;

namespace Experimental.UI.Algorithm.Executor.WPF
{
    public class UIGameExecutor
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private Game konfiguracjaGry;

        public event EventHandler TerminationReached;

        public string AlgorithmState => "TO BE DONE";
        //konfiguracjaGry?
        //.Algorithm?
        //.State
        //.ToString();

        public UIGameExecutor(
            AlgorithmDisplayHelper algorithmDisplayHelper,
            Game dataInput)
        {
            this.algorithmDisplayHelper = algorithmDisplayHelper;
            konfiguracjaGry = dataInput;

            if (konfiguracjaGry.Algorithm == null && konfiguracjaGry.Multialgorithm == null)
                throw new Exception("At least one algorithm has to be set");

            if (konfiguracjaGry.Algorithm != null)
                konfiguracjaGry.Algorithm.QualityCallback += Algorithm_QualityCallback;

            if (konfiguracjaGry.Multialgorithm != null)
                konfiguracjaGry.Multialgorithm.QualityCallback += Algorithm_QualityCallback;
        }

        private void Algorithm_QualityCallback(object? sender, EventArgs e)
        {
            this.algorithmDisplayHelper.Algorithm_Ran(sender, e);
        }

        public void Execute()
        {
            try
            {
                var result = konfiguracjaGry.RunGameAsync<AlgorithmResult>().Result;

                Termination_Reached(result, null);
                this.algorithmDisplayHelper.Algorithm_TerminationReached(result, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Termination_Reached(object? sender, EventArgs e)
        {
            TerminationReached?.Invoke(sender, e);
        }

        public void Pause()
        {
            //if (konfiguracjaGry.Algorithm != null && konfiguracjaGry.Algorithm.IsRunning)
            //    konfiguracjaGry.Algorithm.Stop();
        }

        public void Resume()
        {
            //if (konfiguracjaGry.Algorithm != null && konfiguracjaGry.Algorithm.IsRunning)
            //    konfiguracjaGry.Algorithm.Resume();
        }
    }
}
