using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_simple_board: PrintToConsoleUTBase
    {
        private AlgorithmDebugHelper AlgorithmDebugHelper;
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public I_would_like_to_solve_simple_board(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmDebugHelper = new AlgorithmDebugHelper();
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
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
                foreach(var block in konfiguracjaGry.Blocks)
                {
                    // display definition
                    base.Display(block.BlockAsStringArray());
                    
                    if(showAllowedLocations)
                    {
                        base.Display(string.Empty);
                        base.Display("Block locations START:");
                        foreach(var location in block.AllowedLocations)
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
            konfiguracjaGry.Algorithm.TerminationReached += AlgorithmUTConsoleHelper.Algorithm_TerminationReached;

            konfiguracjaGry.Algorithm.GenerationRan += AlgorithmDebugHelper.Algorithm_Ran;
            konfiguracjaGry.Algorithm.GenerationRan += AlgorithmUTConsoleHelper.Algorithm_Ran;

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
        }
    }
}