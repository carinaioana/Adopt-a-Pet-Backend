using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Queries.GetById
{
    public class GetByIdAnimalQueryHandler : IRequestHandler<GetByIdAnimalQuery, AnimalDto>
    {
        private readonly IAnimalRepository repository;

        public GetByIdAnimalQueryHandler(IAnimalRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AnimalDto> Handle(GetByIdAnimalQuery request, CancellationToken cancellationToken)
        {
            var animal = await repository.FindByIdAsync(request.id);
            if (animal.IsSuccess)
            {
                return new AnimalDto
                {
                    AnimalId = animal.Value.AnimalId,
                    AnimalName = animal.Value.AnimalName,
                    AnimalType = animal.Value.AnimalType,
                    AnimalBreed = animal.Value.AnimalBreed,
                    AnimalAge = animal.Value.AnimalAge,
                    AnimalDescription = animal.Value.AnimalDescription,
                    PersonalityTraits = animal.Value.PersonalityTraits,
                    ImageUrl = animal.Value.ImageUrl,

                };
            }
            return new  AnimalDto();
        }
    }
}
