using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GamePartsController : ControllerBase
    {
        private readonly IGamePartsDetailsService _gamePartsService;
        private readonly ILogger<GamePartsController> _logger;

        public GamePartsController(
            ILogger<GamePartsController> logger,
            IGamePartsDetailsService gamePartsService)
        {
            _logger = logger;
            _gamePartsService = gamePartsService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GamePartsDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var allExisted = _gamePartsService.GetAll();

                if (allExisted == null || allExisted.Count == 0)
                {
                    return NotFound();
                }

                return Ok(allExisted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
