using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.GameParts;

namespace Tangram.Common.GameParts.Boards
{
    internal class PolishMediumBoardData : IGameParts
    {
        private int ScaleFactor = 1;

        private IList<BlockBase> Blocks = new List<BlockBase>()
        {
            DarkBlue.Create(),
            LightBlue.Create(),
            Purple.Create(),
            Blue.Create(),
        };

        private int[] Angles = new int[]
        {
            0,
            90,
            180,
            270
        };

        public GamePartsConfigurator CreateNew(bool withAllowedLocations = false)
        {
            if (withAllowedLocations)
            {
                var modificator = new AllowedLocationsGenerator();
                var preconfiguredBlocks = modificator.Preconfigure(
                    Blocks.ToList(),
                    Board(),
                    Angles);

                return new GamePartsConfigurator(
                    preconfiguredBlocks,
                    Board(),
                    Angles);
            }

            return new GamePartsConfigurator(
                Blocks,
                Board(),
                Angles);
        }

        public BoardShapeBase Board()
        {
            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 5;
            var boardRowsCount = 4;
            var fields = GamePartsFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    ScaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount);

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                ScaleFactor);

            return boardDefinition;
        }
    }
}
