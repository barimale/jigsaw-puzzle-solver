using Algorithm.ConstraintsPairing;
using AutoMapper;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ILogger<ParticipantService> _logger;
        private readonly Engine _engine;
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantService(
            ILogger<ParticipantService> logger,
            IParticipantRepository participantRepoistory,
            IMapper mapper)
        {
            _engine = new Engine();
            _logger = logger;
            _participantRepository = participantRepoistory;
            _mapper = mapper;
        }

        public async Task<Participant> AddAsync(Participant item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ParticipantEntry>(item);
            var added = await _participantRepository.AddAsync(mapped, cancellationToken);

            return _mapper.Map<Participant>(added);
        }

        public async Task<bool> CheckIfEmailAlreadyExist(string eventId, string email, CancellationToken? cancellationToken = null)
        {
            var result = await _participantRepository.CheckIfEmailAlreadyExist(eventId, email, cancellationToken);

            return result.HasValue && result.Value;
        }

        public async Task<bool> CheckIfEmailAlreadyExistEditMode(string eventId, string participantId, string email, CancellationToken? cancellationToken = null)
        {
            var result = await _participantRepository.CheckIfEmailAlreadyExistEditMode(eventId, participantId, email, cancellationToken);

            return result.HasValue && result.Value;
        }

        public async Task<bool> CheckIfNameAlreadyExist(string eventId, string name, CancellationToken? cancellationToken = null)
        {
            var result = await _participantRepository.CheckIfNameAlreadyExist(eventId, name, cancellationToken);

            return result.HasValue && result.Value;
        }

        public async Task<bool> CheckIfNameAlreadyExistEditMode(string eventId, string participantId, string name, CancellationToken? cancellationToken = null)
        {
            var result = await _participantRepository.CheckIfNameAlreadyExistEditMode(eventId, participantId, name, cancellationToken);

            return result.HasValue && result.Value;
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _participantRepository.GetByIdAsync(id, cancellationToken);
            var toReorder = found.Event.Participants.Where(p => p.Id != id);

            var deleted = await _participantRepository.DeleteAsync(id, cancellationToken);

            int i = 1;
            foreach (var existed in toReorder)
            {
                var modified = existed.ExcludedOrderIds.Where(p => p != existed.OrderId).ToArray();
                existed.ExcludedOrderIds = modified.Append(i).ToArray();
                existed.OrderId = i;
                await _participantRepository.UpdateAsync(existed, cancellationToken);
                i = i + 1;
            }

            return deleted;
        }

        public async Task<Participant[]> GetAllAsync(string eventId, CancellationToken? cancellationToken = null)
        {
            var found = await _participantRepository.GetAllAsync(eventId, cancellationToken);

            return found.
                Select(p => _mapper.Map<Participant>(p))
                .ToArray();
        }

        public async Task<Participant> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _participantRepository.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<Participant>(found);
        }

        public async Task<Participant> UpdateAsync(Participant item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ParticipantEntry>(item);
            var updated = await _participantRepository.UpdateAsync(mapped, cancellationToken);

            return _mapper.Map<Participant>(updated);
        }
    }
}
