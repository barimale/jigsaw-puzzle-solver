using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Generic.Algorithm.Tangram.GameParts.Elements.Blocks;
using Generic.Algorithm.Tangram.GameParts.Elements;
using Generic.Algorithm.Tangram.GameParts.Contract;
using Generic.Algorithm.Tangram.GameParts;

namespace Generic.Algorithm.Tangram.GameParts.Elements.Boards.PolishGame
{
    // Modify settings directly in the class.
    internal class MediumBoardData : IGameSet
    {
        private int ScaleFactor = 1;

        private IList<BlockBase> Blocks = new List<BlockBase>()
        {
            Purple.Create(withFieldRestrictions: true),
            DarkBlue.Create(withFieldRestrictions: true),
            LightBlue.Create(withFieldRestrictions: true),
            Blue.Create(withFieldRestrictions: true),
        };

        private int[] Angles = new int[]
        {
            0,
            90,
            180,
            270
        };

        public GameSet CreateNew(bool withAllowedLocations = false)
        {
            if (withAllowedLocations)
            {
                var modificator = new AllowedLocationsGenerator();
                var preconfiguredBlocks = modificator.Preconfigure(
                    Blocks.ToList(),
                    Board(),
                    Angles);

                return new GameSet(
                    preconfiguredBlocks,
                    Board(),
                    Angles);
            }

            return new GameSet(
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
            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    ScaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount,
                    new object[,] {
                        { 1, 0, 1, 0, 1},
                        { 0, 1, 0, 1, 0},
                        { 1, 0, 1, 0, 1},
                        { 0, 1, 0, 1, 0},
                    }
                );

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                ScaleFactor);

            return boardDefinition;
        }
    }
}
