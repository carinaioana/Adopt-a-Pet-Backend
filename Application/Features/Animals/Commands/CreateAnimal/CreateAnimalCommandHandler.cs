using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, CreateAnimalCommandResponse>
    {
        private readonly IAnimalRepository _repository;
        private readonly IMedicalHistoryRepository _medicalHistoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateAnimalCommandHandler(
            IAnimalRepository repository,
            IMedicalHistoryRepository medicalHistoryRepository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _medicalHistoryRepository = medicalHistoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateAnimalCommandResponse> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateAnimalCommandResponse();
            var validator = new CreateAnimalCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            string userId = _currentUserService.GetCurrentUserId();

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
                var animalEntity = animal.Value;
                animalEntity.AttachBreed(request.AnimalBreed);
                animalEntity.AttachDescription(request.AnimalDescription);
                animalEntity.AttachPersonalityTraits(request.PersonalityTraits);
                animalEntity.SetAge(request.AnimalAge);
                animalEntity.AttachImageUrl(request.ImageUrl);
                animalEntity.AttachSex(request.AnimalSex);

                animalEntity.CreatedBy = userId;
                animalEntity.LastModifiedBy = userId;
                animalEntity.CreatedDate = DateTime.UtcNow;
                animalEntity.LastModifiedDate = DateTime.UtcNow;

                var result = await _repository.AddAsync(animalEntity);

                if (result != null)
                {
                    var medicalHistory = MedicalHistory.Create(animal.Value.AnimalId, userId);
                    if (medicalHistory.IsSuccess)
                    {
                        var medicalHistoryEntity = medicalHistory.Value;
                        medicalHistoryEntity.CreatedBy = userId;
                        medicalHistoryEntity.LastModifiedBy = userId;
                        medicalHistoryEntity.CreatedDate = DateTime.UtcNow;
                        medicalHistoryEntity.LastModifiedDate = DateTime.UtcNow;
                        await _medicalHistoryRepository.AddAsync(medicalHistoryEntity);

                        animalEntity.AttachMedicalHistoryId(medicalHistoryEntity.MedicalHistoryId);
                        await _repository.UpdateAsync(animalEntity);

                        return new CreateAnimalCommandResponse
                        {
                            Success = true,
                            Animal = new AnimalDto
                            {
                                AnimalId = animalEntity.AnimalId,
                                AnimalName = animalEntity.AnimalName,
                                AnimalType = animalEntity.AnimalType,
                                AnimalBreed = animalEntity.AnimalBreed,
                                AnimalAge = animalEntity.AnimalAge,
                                AnimalSex = animalEntity.AnimalSex,
                                AnimalDescription = animalEntity.AnimalDescription,
                                PersonalityTraits = animalEntity.PersonalityTraits,
                                ImageUrl = animalEntity.ImageUrl,
                                MedicalHistory = new MedicalHistoryDto
                                {
                                    MedicalHistoryId = medicalHistoryEntity.MedicalHistoryId,
                                    AnimalId = animalEntity.AnimalId,
                                    UserId = userId
                                   
                                }
                            }
                        };
                    }
                }
            }

            return new CreateAnimalCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { animal.Error }
            };
        }
    }
}
