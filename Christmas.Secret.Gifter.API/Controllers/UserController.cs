using Christmas.Secret.Gifter.API.Model;
using Christmas.Secret.Gifter.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthorizeService _authorizeService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(
            ILogger<UserController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IAuthorizeService authorizeService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _authorizeService = authorizeService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync(LoginDetails input)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, isPersistent: false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return StatusCode(400);
                }

                var user = await _userManager.FindByNameAsync(input.Username);

                var token = _authorizeService.GetToken(user);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }
    }
}
