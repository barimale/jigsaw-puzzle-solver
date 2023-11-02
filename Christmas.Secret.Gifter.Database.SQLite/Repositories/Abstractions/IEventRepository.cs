using Christmas.Secret.Gifter.Database.SQLite.Entries;

namespace Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions
{
    public interface IEventRepository: IBaseRepository<EventEntry, string>
    {
        //intentionally left blank
    }
}