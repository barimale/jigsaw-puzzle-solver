using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions.Scoped;
using Christmas.Secret.Gifter.Domain;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IEventService : IBaseRepositoryOuterScope<GiftEvent, string>
    {
        // intentionally left blank
    }
}
