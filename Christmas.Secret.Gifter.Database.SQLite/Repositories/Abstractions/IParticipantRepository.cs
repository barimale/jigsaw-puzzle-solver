using Christmas.Secret.Gifter.Database.SQLite.Entries;

namespace Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions
{
    public interface IParticipantRepository : IBaseRepository<ParticipantEntry, string>
    {
        Task<ParticipantEntry[]> GetAllAsync(string eventId, CancellationToken? cancellationToken = null);
        Task<bool?> CheckIfEmailAlreadyExist(string eventId, string email, CancellationToken? cancellationToken = null);
        Task<bool?> CheckIfNameAlreadyExist(string eventId, string name, CancellationToken? cancellationToken = null);
        Task<bool?> CheckIfEmailAlreadyExistEditMode(string eventId, string participantId, string email, CancellationToken? cancellationToken = null);
        Task<bool?> CheckIfNameAlreadyExistEditMode(string eventId, string participantId, string name, CancellationToken? cancellationToken = null);
    }
}