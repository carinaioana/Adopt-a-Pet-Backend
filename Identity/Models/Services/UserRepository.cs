using AdoptPets.Application.Features.Users;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class ApplicationUserRepository : IUserRepository    
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private ILogger<IUserRepository> _logger;
        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<IUserRepository> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Result<UserDto>> FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<UserDto>.Failure($"User with id {userId} not found");
            }
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }
        public async Task<Result<UserDto>> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<UserDto>.Failure($"User with email {email} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }
        public async Task<Result<UserDto>> FindByUsernameAsync(string username)
        {

            var user = await userManager.FindByNameAsync(username);
            _logger.LogInformation("USERMANAGER user: ", user.Email);
            if (user == null)
                return Result<UserDto>.Failure($"User with username {username} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }


        public async Task<Result<List<UserDto>>> GetAllAsync()
        {
            var users = userManager.Users.ToList();
            var userDtos = users.Select(u => MapToUserDto(u)).ToList();

            foreach (var userDto in userDtos)
            {
                var appUser = await userManager.FindByIdAsync(userDto.UserId.ToString());
                if (appUser != null)
                {
                    var roles = await userManager.GetRolesAsync(appUser);
                    userDto.Roles = roles.ToList();
                }
            }

            return Result<List<UserDto>>.Success(userDtos);
        }

        public async Task<Result<UserDto>> DeleteAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<UserDto>.Failure($"User with id {userId} not found");

            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                return Result<UserDto>.Failure($"User with id {userId} not deleted");

            return Result<UserDto>.Success(MapToUserDto(user));
        }

        public async Task<Result<UserDto>> UpdateAsync(UserDto userDto)
        {
            var userToUpdate = await userManager.FindByIdAsync(userDto.UserId.ToString());
            if (userToUpdate == null)
                return Result<UserDto>.Failure($"User with id {userDto.UserId} not found");

            UpdateUserProperties(userToUpdate, userDto);

            var updateResult = await userManager.UpdateAsync(userToUpdate);
            return updateResult.Succeeded ? Result<UserDto>.Success(MapToUserDto(userToUpdate)) : Result<UserDto>.Failure($"User with id {userDto.UserId} not updated");
        }

        private void UpdateUserProperties(ApplicationUser user, UserDto userDto)
        {
            user.Name = userDto.Name;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
        }
        private UserDto MapToUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                UserId = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Location = user.Location,
                BirthDate = user.BirthDate,
                ProfileImageUrl = user.ProfileImageUrl,
                Description = user.Description

            };
        }


    }
}
