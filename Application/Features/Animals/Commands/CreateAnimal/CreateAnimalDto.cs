namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalDto
    {
        public Guid AnimalId { get; set; }
        public string? AnimalType { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdopted { get; set; }
    }
}