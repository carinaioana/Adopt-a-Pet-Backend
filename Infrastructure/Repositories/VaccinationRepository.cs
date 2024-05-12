using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Infrastructure.Repositories
{
    public class VaccinationRepository : BaseRepository<Vaccination>, IVaccinationRepository
    {
        public VaccinationRepository(AdoptPetsContext context) : base(context)
        {
        }
        public Task<bool> IsVaccineNameAndDateUnique(string vaccineName, DateTime date)
        {
            var matches = context.Vaccinations.Any(a => a.VaccineName.Equals(vaccineName)
            && a.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }
    }

}
