using Genetic.Algorithm.Tangram.GameParts.Boards.BigBoard;
using Genetic.Algorithm.Tangram.Solver.Domain;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Tangram.Common.GameParts.Boards;

namespace Genetic.Algorithm.Tangram.GameParts
{
    public class GamePartsFactory
    {
        public static GeneratorsFactory GeneratorsFactory => new GeneratorsFactory();

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GamePartsConfigurator CreateBigBoard(bool withAllowedLocations = false)
        {
            return new BigBoardData()
                .CreateNew(withAllowedLocations);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GamePartsConfigurator CreateMediumBoard(bool withAllowedLocations = false)
        {
            return new MediumBoardData()
                .CreateNew(withAllowedLocations);
        }

        // no algorithm is pass here in any way.
        // it needs to be added later on.
        public GamePartsConfigurator CreateSimpleBoard(bool withAllowedLocations = false)
        {
            return new SimpleBoardData()
                .CreateNew(withAllowedLocations);
        }
    }
}
