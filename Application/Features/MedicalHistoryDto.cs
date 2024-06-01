using AdoptPets.Application.Models;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features
{
    public class MedicalHistoryDto
    {
        public Guid MedicalHistoryId { get; set; }
        public string UserId { get; set; } = default!;
        public Guid AnimalId { get; set; }
        public AnimalDto Animal { get; set; }

    }
}
