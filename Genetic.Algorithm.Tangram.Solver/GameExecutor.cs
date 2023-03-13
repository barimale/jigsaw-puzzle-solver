using Algorithm.Executor.WPF.Model;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using System;
using System.Collections.Generic;

namespace Genetic.Algorithm.Tangram.Solver.WPF
{
    public class GameExecutor
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private GamePartsConfigurator konfiguracjaGry;
        private IEnumerable<AlgorithmResult> results;

        public event EventHandler GenerationRan;

        public IEnumerable<AlgorithmResult> Results => this.results;

        public string AlgorithmState => this
            .konfiguracjaGry?
            .Algorithm?
            .State
            .ToString();

        public GameExecutor(
            AlgorithmDisplayHelper algorithmDisplayHelper,
            GamePartsConfigurator dataInput)
        {
            this.algorithmDisplayHelper = algorithmDisplayHelper;
            this.konfiguracjaGry = dataInput;

            if (konfiguracjaGry.Algorithm == null)
                throw new System.Exception();

            this.konfiguracjaGry
                .Algorithm
                .TerminationReached += this.algorithmDisplayHelper.Algorithm_TerminationReached;

            this.konfiguracjaGry
                .Algorithm
                .GenerationRan += this.algorithmDisplayHelper.Algorithm_Ran;

            this.konfiguracjaGry
                .Algorithm
                .GenerationRan += this.Algorithm_Ran;

            this.results = new List<AlgorithmResult>();
        }

        public void Execute()
        {
            konfiguracjaGry?.Algorithm?.Start();
            Algorithm_Ran(konfiguracjaGry?.Algorithm, null);
        }

        private void Algorithm_Ran(object? sender, EventArgs e)
        {
            GenerationRan.Invoke(sender, e);
        }

        public void Pause()
        {
            if(konfiguracjaGry.Algorithm != null && konfiguracjaGry.Algorithm.IsRunning)
                konfiguracjaGry.Algorithm.Stop();
        }

        public void Resume()
        {
            if (konfiguracjaGry.Algorithm != null && konfiguracjaGry.Algorithm.IsRunning)
                konfiguracjaGry.Algorithm.Resume();
        }
    }
}
