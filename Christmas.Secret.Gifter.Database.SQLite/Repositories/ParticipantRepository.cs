using AutoMapper;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Christmas.Secret.Gifter.Database.SQLite.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IMapper _mapper;
        private readonly GifterDbContext _context;
        private readonly ILogger<ParticipantRepository> _logger;

        public ParticipantRepository(
            ILogger<ParticipantRepository> logger,
            GifterDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParticipantEntry> AddAsync(ParticipantEntry item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _context.Events.FirstOrDefaultAsync(p => p.Id == item.EventId, cancellationToken);
                if (existedEvent == null)
                {
                    throw new SystemException("Event not found");
                }

                item.EventId = existedEvent.Id;

                var added = await _context.Participants.AddAsync(item, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return added.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Adding failed", ex);
            }
        }

        public async Task<ParticipantEntry> UpdateAsync(ParticipantEntry item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _context
                   .Participants
                   .AsQueryable()
                   .FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);

                if (existed == null)
                {
                    throw new Exception("Entity not found");
                }

                existed.Name = item.Name;
                existed.EventId = item.EventId;
                existed.OrderId = item.OrderId;
                existed.Email = item.Email;
                existed.ExcludedOrderIds = item.ExcludedOrderIds;

                await _context.SaveChangesAsync(cancellationToken);

                var result = _context
                    .Update<ParticipantEntry>(existed);

                await _context.SaveChangesAsync(cancellationToken);

                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Updating failed", ex);
            }
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _context
                    .Participants
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                var deleted = _context
                    .Participants
                    .Remove(existed);

                var result = await _context.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Deleting failed", ex);
            }
        }

        public async Task<ParticipantEntry> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var found = await _context
                    .Participants
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (found == null)
                {
                    return null;
                }

                var mappedResult = _mapper.Map<ParticipantEntry>(found);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<ParticipantEntry[]> GetAllAsync(string eventId, CancellationToken? cancellationToken)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var allOfThem = await _context
                    .Participants
                    .Include(p => p.Event)
                    .ToArrayAsync(cancellationToken ?? default);

                var mapped = allOfThem.Select(p => _mapper.Map<ParticipantEntry>(p));

                return mapped.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<bool?> CheckIfNameAlreadyExist(string eventId, string name, CancellationToken? cancellationToken = null)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var count = await _context
                    .Participants
                    .Include(p => p.Event)
                    .Where(pp => pp.EventId == eventId)
                    .Where(p => p.Name == name)
                    .CountAsync(cancellationToken ?? default);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<bool?> CheckIfEmailAlreadyExist(string eventId, string email, CancellationToken? cancellationToken = null)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var count = await _context
                    .Participants
                    .Include(p => p.Event)
                    .Where(pp => pp.EventId == eventId)
                    .Where(p => p.Email == email)
                    .CountAsync(cancellationToken ?? default);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<bool?> CheckIfEmailAlreadyExistEditMode(string eventId, string participantId, string email, CancellationToken? cancellationToken = null)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var count = await _context
                    .Participants
                    .Include(p => p.Event)
                    .Where(pp => pp.EventId == eventId)
                    .Where(ppp => ppp.Id != participantId)
                    .Where(p => p.Email == email)
                    .CountAsync(cancellationToken ?? default);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<bool?> CheckIfNameAlreadyExistEditMode(string eventId, string participantId, string name, CancellationToken? cancellationToken = null)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var count = await _context
                    .Participants
                    .Include(p => p.Event)
                    .Where(pp => pp.EventId == eventId)
                    .Where(ppp => ppp.Id != participantId)
                    .Where(p => p.Name == name)
                    .CountAsync(cancellationToken ?? default);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
