using Tangram.Solver.UI.Database.SQLite.Repositories.Abstractions.Scoped;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Services.Abstractions
{
    public interface IEventService : IBaseRepositoryOuterScope<GiftEvent, string>
    {
        // intentionally left blank
    }
}
