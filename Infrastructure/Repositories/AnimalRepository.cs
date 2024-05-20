using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdoptPets.Infrastructure.Repositories
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(AdoptPetsContext context) : base(context)
        {

        }

        public Task<List<Animal>> GetAnimalsByUserAsync(string userId)
        {
            return context.Animals
            .Where(a => a.CreatedBy != null && a.CreatedBy == userId)
            .ToListAsync();
        }
    }
}
