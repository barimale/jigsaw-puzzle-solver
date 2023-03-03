using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.WPF
{
    public class GameExecutor
    {
        public void Execute()
        {
            // given
            var konfiguracjaGry = SimpleBoardData.DemoData();

            // settings
            var showAllowedLocations = true;
            var multicolorSupported = false;

            // display board
            base.Display("Board definition:");
            base.Display(konfiguracjaGry.BoardAsStringArray());

            // display blocks
            base.Display("Block definitions:");
            foreach (var block in konfiguracjaGry.Blocks)
            {
                // display definition
                base.Display(block.BlockAsStringArray());

                if (showAllowedLocations)
                {
                    base.Display(string.Empty);
                    base.Display("Block locations START:");
                    foreach (var location in block.AllowedLocations)
                    {
                        // display allowed locations
                        base.Display("Block " + block.Color.ToString() + "definition(allOfThem:" + block.AllowedLocations.Length + ")");
                        base.Display(GamePartsConfigurator.LocationAsStringArray(location));
                    }
                    base.Display("Block locations END.");
                    base.Display(string.Empty);
                }
            }

            konfiguracjaGry.Algorithm.TerminationReached += AlgorithmDebugHelper.Algorithm_TerminationReached;

            konfiguracjaGry.Algorithm.GenerationRan += AlgorithmDebugHelper.Algorithm_Ran;

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
        }
    }
}
