using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, UpdateAnimalViewModel>
    {
        private readonly IAnimalRepository repository;

        public UpdateAnimalCommandHandler(IAnimalRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateAnimalViewModel> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {

            var validator = new UpdateAnimalCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new UpdateAnimalViewModel
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var animal = await repository.FindByIdAsync(request.AnimalId);
            if (animal == null)
            {
                return new UpdateAnimalViewModel
                {
                    Success = false,
                    ValidationsErrors = ["Animal not found"]
                };
            }
            animal.Value.Update(request.AnimalName, request.AnimalType, request.AnimalAge, request.AnimalBreed, request.AnimalSex, request.ImageUrl, request.AnimalDescription, request.PersonalityTraits);
            return new UpdateAnimalViewModel
            {
                Success = true,
                AnimalId = animal.Value.AnimalId,
                AnimalName = animal.Value.AnimalName,
                AnimalType = animal.Value.AnimalType,
                AnimalBreed = animal.Value.AnimalBreed,
                AnimalAge = animal.Value.AnimalAge,
                AnimalSex = animal.Value.AnimalSex,
                AnimalDescription = animal.Value.AnimalDescription,
                PersonalityTraits = animal.Value.PersonalityTraits,
                ImageUrl = animal.Value.ImageUrl,
                
            };
        }
    }
}
