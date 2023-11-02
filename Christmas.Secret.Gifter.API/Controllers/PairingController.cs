using Algorithm.ConstraintsPairing;
using Algorithm.ConstraintsPairing.Model.Requests;
using Algorithm.ConstraintsPairing.Model.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [AllowAnonymous]
    [Route("api/engine/[controller]/[action]")]
    [ApiController]
    public class PairingController : ControllerBase
    {
        private readonly ILogger<PairingController> _logger;
        private readonly Engine _engine;

        private PairingController()
        {
            _engine = new Engine();
        }

        public PairingController(ILogger<PairingController> logger)
            : this()
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlgorithmResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Analyze([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] AlgorithmRequest input, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _engine.CalculateAsync(input.ToInputData());

                return Ok(new AlgorithmResponse()
                {
                    IsError = result.IsError,
                    Reason = result.Reason,
                    Pairs = result.Data.Pairs,
                    AnalysisStatus = result.Data.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
