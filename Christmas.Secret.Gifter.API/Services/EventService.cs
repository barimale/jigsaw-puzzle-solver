using Algorithm.ConstraintsPairing;
using Algorithm.ConstraintsPairing.Model.Requests;
using Algorithm.ConstraintsPairing.Model.Responses;
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
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly Engine _engine;
        private readonly IEventRepository _eventRepoistory;
        private readonly IMapper _mapper;

        public EventService(ILogger<EventService> logger, IEventRepository eventRepoistory, IMapper mapper)
        {
            _engine = new Engine();
            _logger = logger;
            _eventRepoistory = eventRepoistory;
            _mapper = mapper;
        }

        public async Task<GiftEvent> AddAsync(GiftEvent item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<EventEntry>(item);
            var added = await _eventRepoistory.AddAsync(mapped, cancellationToken);

            return _mapper.Map<GiftEvent>(added);
        }

        public async Task<GiftEvent> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _eventRepoistory.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<GiftEvent>(found);
        }

        public async Task<AlgorithmResponse> ExecuteAsync(GiftEvent existed, CancellationToken cancellationToken = default)
        {
            try
            {
                if (existed == null)
                {
                    throw new System.Exception("Event not found. Create the new event first.");
                }

                switch (existed.State)
                {
                    case EventState.CREATED:
                    case EventState.READY_FOR_ANALYZE:
                    case EventState.ANALYZE_IN_PROGRESS:
                    case EventState.COMPLETED_SUCCESSFULLY:
                    case EventState.COMPLETED_FAILY:
                    case EventState.ABANDONED:
                    default:
                        var request = new AlgorithmRequest()
                        {
                            Data = existed.Participants.ToList()
                        };

                        var result = await _engine.CalculateAsync(request.ToInputData());

                        return new AlgorithmResponse()
                        {
                            IsError = result.IsError,
                            Reason = result.Reason,
                            Pairs = result.Data.Pairs,
                            AnalysisStatus = result.Data.Status.ToString()
                        };
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
