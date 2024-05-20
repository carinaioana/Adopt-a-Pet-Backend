using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IAnimalRepository : IAsyncRepository<Animal>
    {
        Task<List<Animal>> GetAnimalsByUserAsync(string userId);

    }
}
