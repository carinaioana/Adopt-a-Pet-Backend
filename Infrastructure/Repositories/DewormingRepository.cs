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

        public async Task<Result<IReadOnlyList<Deworming>>> GetAllByAnimalIdAsync(Guid animalId)
        {
            try
            {
                var observations = await context.Dewormings
                    .Where(o => o.AnimalId == animalId)
                    .Include(o => o.Animal)
                    .AsNoTracking()
                    .ToListAsync(); ;

                if (observations.Count == 0)
                {
                    return Result<IReadOnlyList<Deworming>>.Failure($"No observations found for animal ID {animalId}.");
                }

                return Result<IReadOnlyList<Deworming>>.Success(observations);
            }
            catch (Exception ex)
            {
                return Result<IReadOnlyList<Deworming>>.Failure("Failed to retrieve observations.");
            }
        }
    }
}
