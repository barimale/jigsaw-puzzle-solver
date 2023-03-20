using Tangram.GameParts.Elements.Elements;

namespace Tangram.GameParts.Elements.Contract
{
    public interface IGameSet
    {
        public GameSet CreateNew(bool withAllowedLocations = false);
    }
}
