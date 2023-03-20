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

        public event EventHandler TerminationReached;

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
            TerminationReached.Invoke(sender, e);
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
