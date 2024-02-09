using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;

namespace Tangram.GameParts.Elements.Elements.Boards.PuzzlerPro
{
    /// <summary>
    /// Modify settings directly in the class.
    /// </summary>
    internal class PuzzleProBoardData : IGameSet
    {
        private int ScaleFactor = 1;
        private double fieldHeight = 1d;
        private double fieldWidth = 1d;

        private List<Tuple<string, int>> allowedMatches = new List<Tuple<string, int>>()
        {
            Tuple.Create("X", 1),
            Tuple.Create("O", 0)
        };

        private IList<BlockBase> Blocks = new List<BlockBase>()
        {
            Coralgreen.Create(withFieldRestrictions: true),
            Blue.Create(withFieldRestrictions: true),
            Pink.Create(withFieldRestrictions: true),
            Green.Create(withFieldRestrictions: true),
            Orange.Create(withFieldRestrictions: true),
            Yellow.Create(withFieldRestrictions: true),
            Darkblue.Create(withFieldRestrictions: true),
            Darkred.Create(withFieldRestrictions: true),
            Lightblue.Create(withFieldRestrictions: true),
            Lightgreen.Create(withFieldRestrictions: true),
            Lightred.Create(withFieldRestrictions: true),
            Purple.Create(withFieldRestrictions: true)
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

                var orderedBlocksWithSwap = modificator.ReorderWithSwap(preconfiguredBlocks);

                return new GameSet(
                    orderedBlocksWithSwap,
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
            var boardColumnsCount = 11;
            var boardRowsCount = 5;
            var mesh = new object[,] {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                    };

            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    ScaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount,
                    mesh
                );

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                ScaleFactor)
            {
                AllowedMatches = allowedMatches,
                SkippedMarkup = PolishGameBaseBlock.SkippedMarkup,
                WithExtraRestrictedMarkups = mesh
            };

            return boardDefinition;
        }
    }
}
