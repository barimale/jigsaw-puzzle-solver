using Genetic.Algorithm.Tangram.GameParts.Elements;
using Genetic.Algorithm.Tangram.GameParts.Elements.Boards.PolishGame;
using Genetic.Algorithm.Tangram.GameParts.Elements.Boards.PureGame;
using Genetic.Algorithm.Tangram.Solver.Domain;

namespace Genetic.Algorithm.Tangram.GameParts
{
    public class GameSetFactory
    {
        public static GeneratorFactory GeneratorFactory => new GeneratorFactory();

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GameSet CreateBigBoard(bool withAllowedLocations = false)
        {
            return new BigBoardData()
                .CreateNew(withAllowedLocations);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.

        // TODO: tangramchromosome needs to be refactored to support withAllowedLocations = false
        public GameSet CreatePolishBigBoard(bool withAllowedLocations = false)
        {
            return new PolishBigBoardData()
                .CreateNew(true);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GameSet CreateMediumBoard(bool withAllowedLocations = false)
        {
            return new MediumBoardData()
                .CreateNew(withAllowedLocations);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        // TODO: continue from here as described in the notes
        public GameSet CreatePolishMediumBoard(bool withAllowedLocations = false)
        {
            return new PolishMediumBoardData()
                .CreateNew(true);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GameSet CreateSimpleBoard(bool withAllowedLocations = false)
        {
            return new SimpleBoardData()
                .CreateNew(withAllowedLocations);
        }
    }
}
