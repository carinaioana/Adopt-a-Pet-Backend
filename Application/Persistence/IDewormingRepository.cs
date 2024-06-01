using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IDewormingRepository : IAsyncRepository<Deworming>
    {
        Task<bool> IsDewormingTypeAndDateUnique(string deowrmingType, DateTime date);
        Task<Result<Deworming>> FindByAnimalId(Guid animalId);


    }

}
