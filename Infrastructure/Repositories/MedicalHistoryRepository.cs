using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdoptPets.Infrastructure.Repositories
{
    public class MedicalHistoryRepository : BaseRepository<MedicalHistory>, IMedicalHistoryRepository
    {
        public MedicalHistoryRepository(AdoptPetsContext context) : base(context) { }
        public async Task<Result<MedicalHistory>> FindByAnimalId(Guid animalId)
        {
            var deworming = await context.MedicalHistories
        .FirstOrDefaultAsync(d => d.AnimalId == animalId);

            return deworming != null
                ? Result<MedicalHistory>.Success(deworming)
                : Result<MedicalHistory>.Failure("MedicalHistory not found for the specified animal ID.");

        }
    }
}
