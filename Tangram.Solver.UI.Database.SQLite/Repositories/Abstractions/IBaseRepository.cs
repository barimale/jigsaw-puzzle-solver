using Tangram.Solver.UI.Database.SQLite.Repositories.Abstractions.Scoped;

namespace Tangram.Solver.UI.Database.SQLite.Repositories.Abstractions
{
    public interface IBaseRepository<T, U> : IBaseRepositoryInnerScope<T, U>, IBaseRepositoryOuterScope<T, U>
        where T : class
        where U : class
    {
        // intentionally left blank
    }
}