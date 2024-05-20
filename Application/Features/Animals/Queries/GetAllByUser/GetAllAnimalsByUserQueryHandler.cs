using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Queries.GetAllByUser
{
    public class GetAllAnimalsByUserQueryHandler : IRequestHandler<GetAllAnimalsByUserQuery, GetAllAnimalsByUserResponse>
    {
        private readonly IAnimalRepository repository;
        private readonly ICurrentUserService currentUserService;

        public GetAllAnimalsByUserQueryHandler(IAnimalRepository repository, ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task<GetAllAnimalsByUserResponse> Handle(GetAllAnimalsByUserQuery request, CancellationToken cancellationToken)
        {
            GetAllAnimalsByUserResponse response = new();
            string userId = currentUserService.GetCurrentUserId();

            var animals = await repository.GetAnimalsByUserAsync(userId);
        
            var animalsDtos = animals.Select(a => new AnimalDto
            {
                AnimalId = a.AnimalId,
                AnimalName = a.AnimalName,
                AnimalType = a.AnimalType,
                AnimalBreed = a.AnimalBreed,
                AnimalAge = a.AnimalAge,
                AnimalSex = a.AnimalSex,
                AnimalDescription = a.AnimalDescription,
                PersonalityTraits = a.PersonalityTraits,
                ImageUrl = a.ImageUrl
            }).ToList();
           
            return new GetAllAnimalsByUserResponse
            {
                Animals = animalsDtos,
                Success = true
            };
          
        }
    }
}
