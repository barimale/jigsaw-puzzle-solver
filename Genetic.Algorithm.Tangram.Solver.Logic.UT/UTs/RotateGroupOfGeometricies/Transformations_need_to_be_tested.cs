using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Xunit.Abstractions;
using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    // TODO: remove it 
    public class Transformations_need_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Transformations_need_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task Rotation_of_group_of_polygons()
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

            // when
            var boardPolygons = boardDefinition
                .BoardFieldsDefinition
                .Select(p =>
                {
                    return new GeometryFactory()
                        .CreatePolygon(p.ToCoordinates());
                })
                .ToArray();

            var aa = new GeometryFactory().CreateGeometryCollection(new[] { boardDefinition.Polygon });
        }
    }
}