using Tangram.GameParts.Elements.Elements.Boards.PolishGame;
using Tangram.GameParts.Elements.Elements.Boards.PureGame;
using Tangram.GameParts.Elements.Elements.Boards.PuzzlerPro;
using Tangram.GameParts.Logic;
using Tangram.GameParts.Logic.GameParts;

namespace Tangram.GameParts.Elements
{
    public class GameSetFactory
    {
        public static GeneratorFactory GeneratorFactory => new GeneratorFactory();

        public GameSet CreateBigBoard(bool withAllowedLocations = false)
        {
            return new BigBoardData()
                .CreateNew(withAllowedLocations);
        }

        public GameSet CreatePolishBigBoard(bool withAllowedLocations = false)
        {
            return new PolishBigBoardData()
                .CreateNew(withAllowedLocations);
        }
        
        public GameSet CreatePuzzleProBoardData(bool withAllowedLocations = false)
        {
            return new PuzzleProBoardData()
                .CreateNew(withAllowedLocations);
        }

        public GameSet CreateMediumBoard(bool withAllowedLocations = false)
        {
            return new MediumBoardData()
                .CreateNew(withAllowedLocations);
        }

        public GameSet CreatePolishMediumBoard(bool withAllowedLocations = false)
        {
            return new PolishMediumBoardData()
                .CreateNew(withAllowedLocations);
        }

        public GameSet CreateSimpleBoard(bool withAllowedLocations = false)
        {
            return new SimpleBoardData()
                .CreateNew(withAllowedLocations);
        }
    }
}
