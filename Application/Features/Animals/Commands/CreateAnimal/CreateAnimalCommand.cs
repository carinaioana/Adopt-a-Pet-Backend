using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommand : IRequest<CreateAnimalCommandResponse>
    {

        public string AnimalType { get; set; } = string.Empty;

        public string AnimalName { get; set; } = string.Empty;
        public string? AnimalDescription { get; set; }
        public List<string>? PersonalityTraits { get; set; }
        public int AnimalAge { get; set; }
        public string? AnimalBreed { get; set; }
        public string? AnimalSex { get; set; }
        public string? ImageUrl { get; set; }
       

    }
}
