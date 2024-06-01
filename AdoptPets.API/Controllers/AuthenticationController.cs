using AdoptPets.API.Models;
using AdoptPets.Application.Contracts.Identity;
using AdoptPets.Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using AdoptPets.Application.Contracts.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MediatR;


namespace AdoptPets.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService currentUserService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _logger = logger;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registration(model, UserRoles.User);


                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }
        [Authorize]
        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            var currentUserId = currentUserService.GetCurrentUserId();
            var currentClaimsPrincipal = currentUserService.GetCurrentClaimsPrincipal();

            if (currentUserId == null || currentClaimsPrincipal == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }

            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = currentUserId,
                Claims = currentClaimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
        [Authorize]
        [HttpGet]
        [Route("userinfo/{id}")]
        public async Task<IActionResult> GetUserInfoById(string id)
        {
            try
            {
                var userInfo = await _authService.GetUserInfoById(id);
                if (userInfo == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user info");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching user info" });
            }
        }
       
    }
}
