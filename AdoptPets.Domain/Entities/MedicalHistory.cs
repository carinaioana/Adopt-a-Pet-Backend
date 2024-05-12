using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPets.Domain.Entities
{
    public class MedicalHistory : AuditableEntity
    {
        public MedicalHistory(Guid animalId, Guid userId)
        {
            MedicalHistoryId = Guid.NewGuid();
            AnimalId = animalId;
            UserId = userId;
        }

        [Key]
        public Guid MedicalHistoryId { get; private set; }
        public Guid AnimalId { get; private set; }
        public Animal Animal { get; set; }
        public Guid UserId { get; private set; }

        public static Result<MedicalHistory> Create(Guid animalId, Guid userId)
        {
            if (animalId == Guid.Empty)
            {
                return Result<MedicalHistory>.Failure("AnimalId is required.");
            }
            if (userId == default)
            {
                return Result<MedicalHistory>.Failure("UserId should not be default.");
            }
            return Result<MedicalHistory>.Success(new MedicalHistory(animalId, userId));
        }

    }
}
