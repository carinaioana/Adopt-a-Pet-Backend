using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Result<Vaccination>> FindByAnimalId(Guid animalId)
        {
            var deworming = await context.Vaccinations
        .FirstOrDefaultAsync(d => d.AnimalId == animalId);

            return deworming != null
                ? Result<Vaccination>.Success(deworming)
                : Result<Vaccination>.Failure("Vaccination not found for the specified animal ID.");

        }

        public async Task<Result<IReadOnlyList<Vaccination>>> GetAllByAnimalIdAsync(Guid animalId)
        {
            try
            {
                var observations = await context.Vaccinations
                    .Where(o => o.AnimalId == animalId)
                    .Include(o => o.Animal)
                    .AsNoTracking()
                    .ToListAsync(); ;

                if (observations.Count == 0)
                {
                    return Result<IReadOnlyList<Vaccination>>.Failure($"No observations found for animal ID {animalId}.");
                }

                return Result<IReadOnlyList<Vaccination>>.Success(observations);
            }
            catch (Exception ex)
            {
                return Result<IReadOnlyList<Vaccination>>.Failure("Failed to retrieve observations.");
            }
        }
    }

}
