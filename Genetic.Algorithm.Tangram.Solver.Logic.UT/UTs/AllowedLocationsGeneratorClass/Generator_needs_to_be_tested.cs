using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Tangram.GameParts.Elements;
using Tangram.GameParts.Elements.Elements.Blocks;
using Tangram.GameParts.Logic.Block;
using Tangram.GameParts.Logic.Board;
using Tangram.GameParts.Logic.Generators;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.AllowedLocationsGeneratorClass
{
    public class Generator_needs_to_be_tested : PrintToConsoleUTBase
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
                0,
                90,
                180,
                270
            };

            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 5;
            var boardRowsCount = 4;
            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
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