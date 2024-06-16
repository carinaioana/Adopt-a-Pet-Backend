using AdoptPets.Application.Contracts.Identity;
using AdoptPets.Application.Models.Identity;
using AdoptPets.Application.Persistence;
using Azure.Core;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IS3Service s3Service;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IS3Service s3Service)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.s3Service = s3Service;
        }
        public async Task<(int, string)> Registration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (0, "User already exists");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                //Name = model.Name
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username!);
            if (user == null)
                return (0, "Invalid username");
            if (!await userManager.CheckPasswordAsync(user, model.Password!))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }
        public async Task<(int, string)> Logout()
        {
            await signInManager.SignOutAsync();
            return (1, "User logged out successfully!");
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"]!,
                Audience = configuration["JWT:ValidAudience"]!,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UserInfo> GetUserInfoById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserInfo
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Location = user.Location,
                BirthDate = user.BirthDate,
                Description = user.Description,
                PhoneNumber = user.PhoneNumber,
                ProfilePhoto = user.ProfileImageUrl
            };
        }
        public async Task<(int status, string message)> UpdateEmail(UpdateEmailModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            var existingEmailUser = await userManager.FindByEmailAsync(model.NewEmail);
            if (existingEmailUser != null)
            {
                return (0, "Email already exists.");
            }

            user.Email = model.NewEmail;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "Email updated successfully.");
            }

            return (0, "Failed to update email.");
        }
        public async Task<(int status, string message)> UpdateName(UpdateNameModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.Name = model.NewName;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "Name updated successfully.");
            }

            return (0, "Failed to update name.");
        }
        public async Task<(int status, string message)> UpdateUserName(UpdateUserName model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.UserName = model.NewUserName;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "Username updated successfully.");
            }

            return (0, "Failed to update username.");
        }

        public async Task<(int status, string message)> UpdateLocation(UpdateLocationModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.Location = model.NewLocation;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "Location updated successfully.");
            }

            return (0, "Failed to update location.");
        }

        public async Task<(int status, string message)> UpdateBirthDate(UpdateBirthDateModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.BirthDate = model.NewBirthDate;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "BirthDate updated successfully.");
            }

            return (0, "Failed to update BirthDate.");
        }

        public async Task<(int status, string message)> UpdateProfilePhoto(UpdateProfilePhotoModel model, IFormFile? imageFile)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }
            if (imageFile != null)
            {
                var uploadResult = await s3Service.UploadFileAsync(imageFile);
                if (uploadResult.Success)
                {
                    model.NewProfilePhotoUrl = uploadResult.Url;
                }
                user.ProfileImageUrl = model.NewProfilePhotoUrl;
            }
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                    return (1, "ProfilePhoto updated successfully.");
             }

             return (0, "Failed to update ProfilePhoto.");
            
        }

        public async Task<(int status, string message)> UpdatePhoneNumber(UpdatePhoneNumberModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.PhoneNumber = model.NewPhoneNumber;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "PhoneNumber updated successfully.");
            }

            return (0, "Failed to update PhoneNumber.");
        }

        public async Task<(int status, string message)> UpdateDescription(UpdateDescriptionModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (0, "User not found.");
            }

            user.Description = model.NewDescription;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (1, "Description updated successfully.");
            }

            return (0, "Failed to update Description.");
        }
    }
}