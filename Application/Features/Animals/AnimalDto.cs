
namespace AdoptPets.Application.Features.Animals
{
    public class AnimalDto
    {
        public Guid AnimalId { get; set; }
        public string AnimalType { get; set; } = default!;
        public Guid UserId { get; set; }
        public bool IsAdopted { get; set; }
    }
}
