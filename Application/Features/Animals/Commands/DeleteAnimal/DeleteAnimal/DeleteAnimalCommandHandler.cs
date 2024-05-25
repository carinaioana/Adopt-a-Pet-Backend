using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.DeleteAnimal.DeleteAnimal
{
    public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand, DeleteAnimalCommandResponse>
    {
        private readonly IAsyncRepository<Animal> animalRepository;

        public DeleteAnimalCommandHandler(IAsyncRepository<Animal> animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public async Task<DeleteAnimalCommandResponse> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await animalRepository.DeleteAsync(request.AnimalId);

            if (!deleteResult.IsSuccess)
            {
                return new DeleteAnimalCommandResponse
                {
                    Success = false,
                    Message = deleteResult.Error
                };
            }

            return new DeleteAnimalCommandResponse
            {
                Success = true,
                Message = "Animal deleted successfully."
            };
        }
    }
}
