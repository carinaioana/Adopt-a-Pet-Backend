using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
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
        public async Task<Result<Animal>> FindByNameAsync(string name)
        {
            var propertyInfo = typeof(Animal).GetProperty("AnimalName");
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                return Result<Animal>.Failure("Entity does not have an 'AnimalName' property of type string.");
            }

            try
            {
                var entity = await context.Animals
                    .FirstOrDefaultAsync(e => EF.Property<string>(e, "AnimalName") == name);

                if (entity == null)
                {
                    return Result<Animal>.Failure($"Entity with name '{name}' not found");
                }

                return Result<Animal>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<Animal>.Failure($"An error occurred while retrieving the entity: {ex.Message}");
            }
        }
    }
    
}
