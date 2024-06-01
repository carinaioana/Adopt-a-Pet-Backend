using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdoptPets.Infrastructure.Repositories
{
    public class DewormingRepository : BaseRepository<Deworming>, IDewormingRepository
    {
        public DewormingRepository(AdoptPetsContext context) : base(context)
        {
            // Constructor implementation
        }
        public Task<bool> IsDewormingTypeAndDateUnique(string dewormingType, DateTime date)
        {
            var matches = context.Dewormings.Any(a => a.DewormingType.Equals(dewormingType)
            && a.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }

        public async Task<Result<Deworming>> FindByAnimalId(Guid animalId)
        {
            var deworming = await context.Dewormings
        .FirstOrDefaultAsync(d => d.AnimalId == animalId);

            return deworming != null
                ? Result<Deworming>.Success(deworming)
                : Result<Deworming>.Failure("Deworming not found for the specified animal ID.");

        }
    }
}
