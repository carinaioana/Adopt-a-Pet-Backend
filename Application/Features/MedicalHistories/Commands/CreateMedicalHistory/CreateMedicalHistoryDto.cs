using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryDto
    {
        public Guid MedicalHistoryId { get; set; }
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
/*        public AnimalDto Animal { get; set; }
*/

    }
}
