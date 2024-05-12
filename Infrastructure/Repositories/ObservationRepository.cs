using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

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
    }
}
