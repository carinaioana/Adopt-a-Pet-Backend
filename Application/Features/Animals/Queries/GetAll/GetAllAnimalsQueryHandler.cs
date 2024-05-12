using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Queries.GetAll
{
    public class GetAllAnimalsQueryHandler : IRequestHandler<GetAllAnimalsQuery, GetAllAnimalsResponse>
    {
        private readonly IAnimalRepository repository;

        public GetAllAnimalsQueryHandler(IAnimalRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllAnimalsResponse> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
        {
            GetAllAnimalsResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess) 
            {
                response.Animals = result.Value.Select(animal => new AnimalDto
                {
                    AnimalId = animal.AnimalId,
                    AnimalName = animal.AnimalName,
                    AnimalType = animal.AnimalType,
                    AnimalBreed = animal.AnimalBreed,
                    AnimalAge = animal.AnimalAge,
                    AnimalSex = animal.AnimalSex,
                    AnimalDescription = animal.AnimalDescription,
                    PersonalityTraits = animal.PersonalityTraits,
                    ImageUrl = animal.ImageUrl,
                }).ToList();
            }
            return response;
        }
    }
}
