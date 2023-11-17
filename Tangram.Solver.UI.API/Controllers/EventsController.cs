using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using PubSub;
using System.Threading;
using System.Threading.Tasks;
using Tangram.Solver.UI.Services.Abstractions;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;
        private readonly Hub _hub; // to hosted service via PubSub

        public EventsController(
            ILogger<EventsController> logger,
            IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
            _hub = Hub.Default;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GiftEvent))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var newEvent = new GiftEvent();

                var created = await _eventService.AddAsync(newEvent, cancellationToken);
                _hub.Publish(created);

                return Ok(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{eventId}/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameSettings))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(
            string eventId,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GameSettings input,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                // TODO save both ids
                var existed = await _eventService.GetByIdAsync(eventId, cancellationToken);

                _hub.Publish(input);

                if (existed == null)
                {
                    return NotFound();
                }

                // save data to db
                var result = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GiftEvent))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _eventService.GetByIdAsync(id, cancellationToken);

                if (existed == null)
                {
                    return NotFound();
                }

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
