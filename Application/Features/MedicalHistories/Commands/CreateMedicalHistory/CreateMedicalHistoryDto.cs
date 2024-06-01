using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryDto
    {
        public Guid MedicalHistoryId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }
    }
}
