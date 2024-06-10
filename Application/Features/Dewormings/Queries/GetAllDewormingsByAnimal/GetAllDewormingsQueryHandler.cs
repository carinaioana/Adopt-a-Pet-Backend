using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetAllDewormingsByAnimal
{
    public class GetAllDewormingsQueryHandler : IRequestHandler<GetAllDewormingsQuery, GetAllDewormingsResponse>
    {
        private readonly IDewormingRepository repository;

        public GetAllDewormingsQueryHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllDewormingsResponse> Handle(GetAllDewormingsQuery request, CancellationToken cancellationToken)
        {
            GetAllDewormingsResponse response = new();
            var result = await repository.GetAllByAnimalIdAsync(request.AnimalId);
            if (result.IsSuccess)
            {
                response.Dewormings = result.Value.Select(deworming => new DewormingDto
                {
                    DewormingId = deworming.DewormingId,
                    Date = deworming.Date,
                    DewormingType = deworming.DewormingType,
                    AnimalId = deworming.AnimalId,
                }).ToList();
            }
            return response;
        }
    }
}
