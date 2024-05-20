using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, CreateAnimalCommandResponse>
    {
        private readonly IAnimalRepository _repository;
        private readonly ICurrentUserService currentUserService;

        public CreateAnimalCommandHandler(IAnimalRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task<CreateAnimalCommandResponse> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateAnimalCommandResponse();
            var validator = new CreateAnimalCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            string userId = currentUserService.GetCurrentUserId();

            if (!validatorResult.IsValid)
            {
                return new CreateAnimalCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var animal = Animal.Create(request.AnimalName, request.AnimalType);
            if (animal.IsSuccess)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                animal.Value.AttachBreed(request.AnimalBreed);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                animal.Value.AttachDescription(request.AnimalDescription);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                animal.Value.AttachPersonalityTraits(request.PersonalityTraits);
#pragma warning restore CS8604 // Possible null reference argument.
                animal.Value.SetAge(request.AnimalAge);
#pragma warning disable CS8604 // Possible null reference argument.
                animal.Value.AttachImageUrl(request.ImageUrl);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                animal.Value.AttachSex(request.AnimalSex);
#pragma warning restore CS8604 // Possible null reference argument.

                animal.Value.CreatedBy = userId;
                animal.Value.LastModifiedBy = userId;
                animal.Value.CreatedDate = DateTime.UtcNow;
                animal.Value.LastModifiedDate = DateTime.UtcNow;

                var result =  _repository.AddAsync(animal.Value);


                return new CreateAnimalCommandResponse
                {
                    Success = true,
                    Animal = new CreateAnimalDto
                    {
                        AnimalId = animal.Value.AnimalId,
                        AnimalName = animal.Value.AnimalName,
                        AnimalType = animal.Value.AnimalType,
                        AnimalBreed = animal.Value.AnimalBreed,
                        AnimalAge = animal.Value.AnimalAge,
                        AnimalSex = animal.Value.AnimalSex,
                        AnimalDescription = animal.Value.AnimalDescription,
                        PersonalityTraits = animal.Value.PersonalityTraits,
                        ImageUrl = animal.Value.ImageUrl,

                    }
                };
            }
            return new CreateAnimalCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { animal.Error }
                };
            }
    }
}
