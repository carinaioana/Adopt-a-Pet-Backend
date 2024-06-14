using AdoptPets.Application.Features.Users;
using AdoptPets.Domain.Common;

namespace AdoptPets.Application.Persistence
{
    public interface  IUserRepository
    {
        Task<Result<UserDto>> FindByIdAsync(string userId);
        Task<Result<UserDto>> FindByEmailAsync(string email);
        Task<Result<UserDto>> FindByUsernameAsync(string username);
        Task<Result<List<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> DeleteAsync(Guid userId);
        //Task<Result<UserDto>> UpdateAsync(UserDto user);
    }
}
