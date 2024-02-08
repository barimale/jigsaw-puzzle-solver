using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro;

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

        private IList<BlockBase> Blocks = new List<BlockBase>()
        {
            Coralgreen.Create(withFieldRestrictions: false),
            Blue.Create(withFieldRestrictions: false),
            Pink.Create(withFieldRestrictions: false),
            Green.Create(withFieldRestrictions: false),
            Orange.Create(withFieldRestrictions: false),
            Yellow.Create(withFieldRestrictions: false),
            Darkblue.Create(withFieldRestrictions: false),
            Darkred.Create(withFieldRestrictions: false),
            Lightblue.Create(withFieldRestrictions: false),
            Lightgreen.Create(withFieldRestrictions: false),
            Lightred.Create(withFieldRestrictions: false),
            Purple.Create(withFieldRestrictions: false)
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
            var modificator = new AllowedLocationsGenerator(
                        null,
                        fieldHeight,
                        fieldWidth
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

        private BoardShapeBase Board()
        {
            var boardColumnsCount = 11;
            var boardRowsCount = 5;

            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    ScaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount                );

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                ScaleFactor);

            return boardDefinition;
        }
    }
}
