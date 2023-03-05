using Genetic.Algorithm.Tangram.Solver.Domain;

namespace Genetic.Algorithm.Tangram.GameParts
{
    public interface IGameParts
    {
        public GamePartsConfigurator CreateNew(bool withAllowedLocations = false);
    }
}
