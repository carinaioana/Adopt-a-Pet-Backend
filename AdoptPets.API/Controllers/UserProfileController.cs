using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Features.Users;
using AdoptPets.Application.Features.Users.UpdateUser;
using AdoptPets.Application.Persistence;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize] // Apply authorization to the entire controller
    public class UserProfileController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IUserRepository> logger;
        private readonly ICurrentUserService currentUserService;
        private readonly IUserRepository repository;

        public UserProfileController(UserManager<ApplicationUser> userManager, ILogger<IUserRepository> logger,ICurrentUserService currentUserService, IUserRepository repository)
        {
            _userManager = userManager;
            this.logger = logger;
            this.currentUserService = currentUserService;
            this.repository = repository;
        }



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            try
            {
                var userProfile = await _userManager.FindByIdAsync(id);
                if (userProfile == null)
                {
                    return NotFound(new { message = "User profile not found" });
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user profile");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching user profile" });
            }
        }
/*
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, UpdateUserCommand command)
        {
            if (id != command.UserId)
                return BadRequest(new { message = "User ID mismatch" });

            var userDto = new UserDto
            {
                UserId = command.UserId,
                Name = command.Name,
                UserName = command.UserName,
                Email = command.Email
            };

            var result = await repository.UpdateAsync(userDto); // Assuming you inject IUserRepository

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(new { message = result.Error });
        }*/
        /*
                [HttpPut("{id}")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                public async Task<IActionResult> Update(string id, UpdateUserCommand command)
                {
                    if (id != command.Id)
                    {
                        return BadRequest("The ID in the URL does not match the ID in the command.");
                    }

                    var result = await Mediator.Send(command);

                    if (!result.Success)
                    {
                        if (result.Message == "User ID not found")
                        {
                            return NotFound(result.Message);
                        }

                        return BadRequest(result.Message);
                    }

                    return Ok(result.Message);
                }*/

    }
}

