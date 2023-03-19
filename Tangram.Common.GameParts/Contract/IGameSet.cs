using Genetic.Algorithm.Tangram.GameParts.Elements;

namespace Genetic.Algorithm.Tangram.GameParts.Contract
{
    public interface IGameSet
    {
        public GameSet CreateNew(bool withAllowedLocations = false);
    }
}
