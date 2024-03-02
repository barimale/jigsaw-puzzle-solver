using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Elements.Elements.Blocks.XOXO;

namespace Tangram.GameParts.Elements.Elements.Boards.PureGame
{
    /// <summary>
    /// Modify settings directly in the class.
    /// </summary>
    internal class MediumBoardData : IGameSet
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
