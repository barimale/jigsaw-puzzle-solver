using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using TreesearchLib;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.TreeSearches.MCTS
{
    // WIP
    public class Dummy_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Dummy_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task With_two_blocks()
        {
            // given
            int scaleFactor = 1;
            var fieldHeight = 1d;
            var fieldWidth = 1d;

            IList<BlockBase> blocks = new List<BlockBase>()
            {
                Purple.Create(withFieldRestrictions: true),
                Black.Create(withFieldRestrictions: true),
            };

            int[] angles = new int[]
            {
                0,
                90,
                180,
                270
            };

            var boardColumnsCount = 3;
            var boardRowsCount = 2;
            var fields = GamePartsFactory
                .GeneratorFactory
                .RectangularBoardFieldsGenerator
                .GenerateFields(
                    scaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount,
                    new object[,] { { 1, 0, 1 }, { 0, 1, 0 } }
                );

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                scaleFactor);

            // leftTuple - blockMarkup
            // rightTuple - boardMarkup
            var allowedMatches = new List<Tuple<string, int>>()
            {
                Tuple.Create("O", 1),
                Tuple.Create("X", 0)
            };

            var modificator = new AllowedLocationsGenerator(
                allowedMatches,
                new List<object>()
                {
                    Purple.SkippedMarkup
                });

            var preconfiguredBlocks = modificator.Preconfigure(
                    blocks.ToList(),
                    boardDefinition,
                    angles);

            // when
            int size = preconfiguredBlocks.Count;

            // then
            var solution = new FindFittestSolution(
                size,
                boardDefinition,
                preconfiguredBlocks);

            var pilot = solution.PilotMethodAsync();

            var results = await Task.WhenAll(new Task<FindFittestSolution>[]
            {
                pilot
            });

            // finally
            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0]);
        }
    }
}