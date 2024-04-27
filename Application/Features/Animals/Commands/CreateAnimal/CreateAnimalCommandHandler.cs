using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, CreateAnimalCommandResponse>
    {
        private readonly IAnimalRepository _repository;

        public CreateAnimalCommandHandler(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateAnimalCommandResponse> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateAnimalCommandResponse();
            var validator = new CreateAnimalCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationsErrors = validatorResult.Errors.Select(error => error.ErrorMessage).ToList();
            }

            if (response.Success)
            {
                var animal = Animal.Create(request.AnimalType, request.UserId);
                // Domain
                // Entity validation
                if (animal.IsSuccess)
                {
                    await _repository.AddAsync(animal.Value);
                    response.Animal = new CreateAnimalDto
                    {
                        AnimalId = animal.Value.AnimalId,
                        AnimalType = animal.Value.AnimalType,
                        UserId = animal.Value.UserId,
                        IsAdopted = animal.Value.IsAdopted
                    };
                }
                else
                {
                    response.Success = false;
                    response.ValidationsErrors = new List<string> { animal.Error };
                }
            }

            return response;
        }
    }
}
