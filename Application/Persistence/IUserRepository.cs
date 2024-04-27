using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
