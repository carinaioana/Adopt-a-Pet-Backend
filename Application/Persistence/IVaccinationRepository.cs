using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IVaccinationRepository : IAsyncRepository<Vaccination>
    {
        Task<bool> IsVaccineNameAndDateUnique(string vaccineName, DateTime date);
        Task<Result<Vaccination>> FindByAnimalId(Guid animalId);

    }
}
