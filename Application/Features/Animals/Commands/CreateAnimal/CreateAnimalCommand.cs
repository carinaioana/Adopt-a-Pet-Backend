using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommand : IRequest<CreateAnimalCommandResponse>
    {
        public string AnimalType { get; set; } = default!;
        public Guid UserId { get; set; }
        public bool IsAdopted { get; set; }


    }
}
