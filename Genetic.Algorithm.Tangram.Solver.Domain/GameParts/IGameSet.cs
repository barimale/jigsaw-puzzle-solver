namespace Tangram.GameParts.Logic.GameParts
{
    public interface IGameSet
    {
        public GameSet CreateNew(bool withAllowedLocations = false);
    }
}
