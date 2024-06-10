using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Features.Dewormings.Queries.GetByIdDeworming;
using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetDewormingByAnimal
{
    public class GetByAnimalDewormingQueryHandler : IRequestHandler<GetByAnimalDewormingQuery, GetByAnimalDewormingQueryResponse>
    {
        private readonly IDewormingRepository repository;

        public GetByAnimalDewormingQueryHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetByAnimalDewormingQueryResponse> Handle(GetByAnimalDewormingQuery request, CancellationToken cancellationToken)
        {
            var deworming = await repository.FindByAnimalId(request.AnimalId);
            if (!deworming.IsSuccess)
            {
                return new GetByAnimalDewormingQueryResponse
                {
                    Success = false,
                    ValidationsErrors = [deworming.Error]
                };
            }

            return new GetByAnimalDewormingQueryResponse
            {
                Success = true,
                DewormingDto = new CreateDewormingDto
                {
                    DewormingId = deworming.Value.DewormingId,
                    DewormingType = deworming.Value.DewormingType,
                    Date = deworming.Value.Date,
                    AnimalId = deworming.Value.AnimalId
                }
            };

        }
    }
}
