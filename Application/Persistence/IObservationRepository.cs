using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IObservationRepository : IAsyncRepository<Observation>
    {
         Task<bool> IsObservationDescriptionAndDateUnique(string observationDescription, DateTime date);
    }

}
