using Generic.Algorithm.Tangram.GameParts.Elements;

namespace Generic.Algorithm.Tangram.GameParts.Contract
{
    public interface IGameSet
    {
        public GameSet CreateNew(bool withAllowedLocations = false);
    }
}
