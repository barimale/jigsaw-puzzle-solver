using System.Drawing;
using NetTopologySuite.Geometries;
using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Boards.PureGame
{
    internal class SimpleBoardData : IGameSet
    {
        private int ScaleFactor = 1;

        private int[] Angles = new int[]
        {
            0,
            90,
            180,
            270
        };

        private IList<BlockBase> Blocks()
        {
            // blocks
            var blocks = new List<BlockBase>();
            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                new Coordinate(0,0),// first the same as last
                new Coordinate(0,2),
                new Coordinate(1,2),
                new Coordinate(1,0),
                new Coordinate(0,0)// last the same as first
                });
            var zielonyBloczek = new BlockBase(polygon, Color.Green);
            var toStringFromClone1 = zielonyBloczek.ToString();

            blocks.Add(zielonyBloczek);

            var polygon2 = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                new Coordinate(0,0),// first the same as last
                new Coordinate(0,1),
                new Coordinate(1,1),
                new Coordinate(1,0),
                new Coordinate(0,0)// last the same as first
                });
            var niebieskiBloczek = new BlockBase(polygon2, Color.Blue);
            var toStringFromClone2 = niebieskiBloczek.ToString();

            blocks.Add(niebieskiBloczek);

            var polygon3 = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,1),
                    new Coordinate(1,1),
                    new Coordinate(1,0),
                    new Coordinate(0,0)// last the same as first
                });
            var czerwonyBloczek = new BlockBase(polygon3, Color.Red);
            var toStringFromClone3 = czerwonyBloczek.ToString();

            blocks.Add(czerwonyBloczek);

            return blocks;
        }

        public GameSet CreateNew(bool withAllowedLocations = false)
        {
            if (withAllowedLocations)
            {
                var modificator = new AllowedLocationsGenerator();
                var preconfiguredBlocks = modificator.Preconfigure(
                    Blocks().ToList(),
                    Board(),
                    Angles);

                return new GameSet(
                    preconfiguredBlocks,
                    Board(),
                    Angles);
            }

            return new GameSet(
                Blocks(),
                Board(),
                Angles);
        }

        private BoardShapeBase Board()
        {
            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var fields = new List<BoardFieldDefinition>()
            {
                new BoardFieldDefinition(0,0,fieldWidth, fieldHeight, true, ScaleFactor),
                new BoardFieldDefinition(0,1,fieldWidth, fieldHeight, true, ScaleFactor),
                new BoardFieldDefinition(1,1,fieldWidth, fieldHeight, true, ScaleFactor),
                new BoardFieldDefinition(1,0,fieldWidth, fieldHeight, true, ScaleFactor)
            };
            BoardShapeBase boardDefinition = new BoardShapeBase(fields, 2, 2, ScaleFactor);

            return boardDefinition;
        }
    }
}
