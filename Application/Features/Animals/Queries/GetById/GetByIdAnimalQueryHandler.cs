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
                    AnimalType = animal.Value.AnimalType,
                    UserId = animal.Value.UserId,
                    IsAdopted = animal.Value.IsAdopted
                };
            }
            return new  AnimalDto();
        }
    }
}
