using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, UpdateAnimalCommandResponse>
    {
        private readonly IAnimalRepository repository;

        public UpdateAnimalCommandHandler(IAnimalRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateAnimalCommandResponse> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {

            var validator = new UpdateAnimalCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateAnimalCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var animalResult = await repository.FindByIdAsync(request.AnimalId);
            if (!animalResult.IsSuccess)
            {
                return new UpdateAnimalCommandResponse
                {
                    Success = false,
                    Message = "Animal not found"
                };
            }
            var animal = animalResult.Value;

           if(request.AnimalName != null)
            {
                animal.UpdateName(request.AnimalName);
            }
            if (request.AnimalType != null)
            {
                animal.UpdateType(request.AnimalType);
            }
            if (request.AnimalDescription != null)
            {
                animal.UpdateDescription(request.AnimalDescription);
            }
            if (request.PersonalityTraits != null)
            {
                animal.UpdatePersonalityTraits(request.PersonalityTraits);
            }
            if (request.AnimalAge != 0)
            {
                animal.UpdateAge(request.AnimalAge);
            }
            if (request.AnimalBreed != null)
            {
                animal.UpdateBreed(request.AnimalBreed);
            }
            if (request.ImageUrl != null)
            {
                animal.UpdateImageUrl(request.ImageUrl);
            }
            if (request.AnimalSex != null)
            {
                animal.UpdateSex(request.AnimalSex);
            }

            var updateResult = await repository.UpdateAsync(animal);
            if(!updateResult.IsSuccess)
            {
                return new UpdateAnimalCommandResponse
                {
                    Success = false,
                    Message = "Error updating animal"
                };
            }
            return new UpdateAnimalCommandResponse
            {
                Success = true,
                Message = "Animal updated successfully"
            };
        }
    }
}
