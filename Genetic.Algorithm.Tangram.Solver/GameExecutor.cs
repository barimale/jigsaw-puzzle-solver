using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.Game.Logic;
using System;
using System.Collections.Generic;

namespace Experimental.UI.Algorithm.Executor.WPF
{
    public class UIGameManager
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private Game konfiguracjaGry;
        private IEnumerable<AlgorithmResult> results;

        public event EventHandler GenerationRan;

        public IEnumerable<AlgorithmResult> Results => results;

        public string AlgorithmState => "TO BE DONE";
        //konfiguracjaGry?
        //.Algorithm?
        //.State
        //.ToString();

        public UIGameManager(
            AlgorithmDisplayHelper algorithmDisplayHelper,
            Game dataInput)
        {
            this.algorithmDisplayHelper = algorithmDisplayHelper;
            konfiguracjaGry = dataInput;

            if (konfiguracjaGry.Algorithm == null)
                throw new Exception();

            // TODO WIP 
            //konfiguracjaGry
            //    .Algorithm
            //    .TerminationReached += this.algorithmDisplayHelper.Algorithm_TerminationReached;

            //konfiguracjaGry
            //    .Algorithm
            //    .GenerationRan += this.algorithmDisplayHelper.Algorithm_Ran;

            //konfiguracjaGry
            //    .Algorithm
            //    .GenerationRan += this.Algorithm_Ran;
            results = new List<AlgorithmResult>();
        }

        public void Execute()
        {

            //konfiguracjaGry?.Algorithm?.Start();
            var result = konfiguracjaGry.RunGameAsync<AlgorithmResult>().Result;

            Algorithm_Ran(result, null);
            this.algorithmDisplayHelper.Algorithm_TerminationReached(result, null);
        }

        private void Algorithm_Ran(object? sender, EventArgs e)
        {
            GenerationRan.Invoke(sender, e);
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
