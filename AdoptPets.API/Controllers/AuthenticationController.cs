﻿using AdoptPets.API.Models;
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
        [HttpPut]
        [Route("update-email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmail(UpdateEmailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateEmail(model);

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
        [HttpPut]
        [Route("update-name")]
        [Authorize]
        public async Task<IActionResult> UpdateName(UpdateNameModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateName(model);

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
        [HttpPut]
        [Route("update-username")]
        [Authorize]
        public async Task<IActionResult> UpdateUserName(UpdateUserName model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateUserName(model);

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
        [HttpPut]
        [Route("update-location")]
        [Authorize]
        public async Task<IActionResult> UpdateLocation(UpdateLocationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateLocation(model);

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
        [HttpPut]
        [Route("update-birthDate")]
        [Authorize]
        public async Task<IActionResult> UpdateBirthDate(UpdateBirthDateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateBirthDate(model);

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
        [HttpPut]
        [Route("update-description")]
        [Authorize]
        public async Task<IActionResult> UpdateDescription(UpdateDescriptionModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateDescription(model);

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
        [HttpPut]
        [Route("update-phoneNumber")]
        [Authorize]
        public async Task<IActionResult> UpdatePhoneNumber(UpdatePhoneNumberModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdatePhoneNumber(model);

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
        [HttpPut]
        [Route("update-profilePhoto")]
        [Authorize]
        public async Task<IActionResult> UpdateProfilePhoto([FromForm] UpdateProfilePhotoModel model, IFormFile? imageFile)
        {
            model.ImageFile = imageFile;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.UpdateProfilePhoto(model, imageFile);

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
     
    }
}
