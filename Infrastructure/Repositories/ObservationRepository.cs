using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdoptPets.Infrastructure.Repositories
{
    public class ObservationRepository : BaseRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(AdoptPetsContext context) : base(context)
        {
            // Constructor implementation
        }
        public Task<bool> IsObservationDescriptionAndDateUnique(string observationDescription, DateTime date)
        {
            var matches = context.Observations.Any(a => a.ObservationDescription.Equals(observationDescription)
            && a.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }
        public async Task<Result<Observation>> FindByAnimalId(Guid animalId)
        {
            var deworming = await context.Observations
        .FirstOrDefaultAsync(d => d.AnimalId == animalId);

            return deworming != null
                ? Result<Observation>.Success(deworming)
                : Result<Observation>.Failure("Observation not found for the specified animal ID.");

        }
    }
}
