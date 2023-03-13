using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class Generator_needs_to_be_tested: PrintToConsoleUTBase
    {
        public Generator_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public void Check_if_all_possible_locations_are_generated()
        {
            // given
            int scaleFactor = 1;

            IList<BlockBase> blocks = new List<BlockBase>()
            {
                Purple.Create()
            };

            int[] angles = new int[]
            {
                -270,
                -180,
                -90,
                0,
                90,
                180,
                270
            };

            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 5;
            var boardRowsCount = 4;
            var fields = GamePartsFactory
                .GeneratorsFactory
                .FieldsGenerator
                .GenerateFields(
                    scaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount);

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                scaleFactor);

            // when
            var modificator = new AllowedLocationsGenerator();
            var preconfiguredBlocks = modificator.Preconfigure(
                blocks.ToList(),
                boardDefinition,
                angles);

            // then
            Assert.Equal(34, preconfiguredBlocks
                .FirstOrDefault()?
                .AllowedLocations
                .Count());
        }
    }
}