using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IMedicalHistoryRepository : IAsyncRepository<MedicalHistory>
    {
        Task<Result<MedicalHistory>> FindByAnimalId(Guid animalId);


    }
}
