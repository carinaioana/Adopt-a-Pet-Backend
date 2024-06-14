using AdoptPets.Application.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace AdoptPets.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, string)> Logout();
        Task<UserInfo> GetUserInfoById(string userId);
        Task<(int status, string message)> UpdateEmail(UpdateEmailModel model);
        Task<(int status, string message)> UpdateName(UpdateNameModel model);
        Task<(int status, string message)> UpdateUserName(UpdateUserName model);
        Task<(int status, string message)> UpdateLocation(UpdateLocationModel model);

        Task<(int status, string message)> UpdateBirthDate(UpdateBirthDateModel model);

        Task<(int status, string message)> UpdateProfilePhoto(UpdateProfilePhotoModel model, IFormFile? imageFile);
        Task<(int status, string message)> UpdatePhoneNumber(UpdatePhoneNumberModel model);
        Task<(int status, string message)> UpdateDescription(UpdateDescriptionModel model);


    }
}
