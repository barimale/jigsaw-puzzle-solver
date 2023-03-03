using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.WPF
{
    public class GameExecutor
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private GamePartsConfigurator konfiguracjaGry;
        public GameExecutor(
            AlgorithmDisplayHelper algorithmDisplayHelper,
            GamePartsConfigurator dataInput)
        {
            this.algorithmDisplayHelper = algorithmDisplayHelper;
            this.konfiguracjaGry = dataInput;
            this.konfiguracjaGry.Algorithm.TerminationReached += this.algorithmDisplayHelper.Algorithm_TerminationReached;
            this.konfiguracjaGry.Algorithm.GenerationRan += this.algorithmDisplayHelper.Algorithm_Ran;
        }

        public void Execute()
        {
            konfiguracjaGry.Algorithm.Start();
        }

        public void Pause()
        {
            konfiguracjaGry.Algorithm.Stop();
        }

        public void Resume()
        {
            konfiguracjaGry.Algorithm.Resume();
        }
    }
}
