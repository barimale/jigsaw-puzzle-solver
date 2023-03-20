using Tangram.GameParts.Elements.Elements.Blocks;
using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;

namespace Tangram.GameParts.Elements.Elements.Boards.PolishGame
{
    /// <summary>
    /// Modify settings directly in the class.
    /// </summary>
    internal class PolishBigBoardData : IGameSet
    {
        private int ScaleFactor = 1;
        private double fieldHeight = 1d;
        private double fieldWidth = 1d;

        private List<Tuple<string, int>> allowedMatches = new List<Tuple<string, int>>()
        {
            Tuple.Create("O", 1),
            Tuple.Create("X", 0)
        };

        private IList<BlockBase> Blocks = new List<BlockBase>()
        {
            DarkBlue.Create(withFieldRestrictions: true),
            Red.Create(withFieldRestrictions: true),
            LightBlue.Create(withFieldRestrictions: true),
            Purple.Create(withFieldRestrictions: true),
            Blue.Create(withFieldRestrictions: true),
            Pink.Create(withFieldRestrictions: true),
            Green.Create(withFieldRestrictions: true),
            LightGreen.Create(withFieldRestrictions: true),
            Orange.Create(withFieldRestrictions: true),
            Yellow.Create(withFieldRestrictions: true)
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
                var modificator = new AllowedLocationsGenerator(
                        allowedMatches,
                        fieldHeight,
                        fieldWidth,
                        new List<object>() { PolishGameBaseBlock.SkippedMarkup }
                    );

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

        private BoardShapeBase Board()
        {
            var boardColumnsCount = 10;
            var boardRowsCount = 5;
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
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                        { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                        { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 }
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
