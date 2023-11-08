using AutoMapper;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Christmas.Secret.Gifter.Database.SQLite.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly IMapper _mapper;
        private readonly GifterDbContext _context;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(
            ILogger<EventRepository> logger,
            GifterDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventEntry> AddAsync(EventEntry item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _context
                    .Events
                    .AddAsync(item, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<EventEntry>(result.Entity);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Adding failed", ex);
            }
        }

        public async Task<EventEntry> UpdateAsync(EventEntry item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _context
                   .Events
                   .Include(p => p.Participants)
                   .AsQueryable()
                   .FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);

                if (existed == null)
                {
                    throw new Exception("Entity not found");
                }

                var mapped = _mapper.Map(item, existed);
                var result = _context.Update(mapped);

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
                    .Events
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                var deleted = _context
                    .Events
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

        public async Task<EventEntry> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var found = await _context
                    .Events
                    .Include(p => p.Participants)
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (found == null)
                {
                    return null;
                }

                var mappedResult = _mapper.Map<EventEntry>(found);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
