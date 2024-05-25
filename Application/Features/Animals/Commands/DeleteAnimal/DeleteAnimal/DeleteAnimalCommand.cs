using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.DeleteAnimal.DeleteAnimal
{
    public class DeleteAnimalCommand : IRequest<DeleteAnimalCommandResponse>
    {
        public Guid AnimalId { get; set; }
    }
}
