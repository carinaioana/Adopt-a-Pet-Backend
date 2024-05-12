using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Infrastructure.Repositories
{
    public class MedicalHistoryRepository : BaseRepository<MedicalHistory>, IMedicalHistoryRepository
    {
        public MedicalHistoryRepository(AdoptPetsContext context) : base(context) { }
    }
}
