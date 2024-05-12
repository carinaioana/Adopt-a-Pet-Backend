using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetAllDewormings
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
            var result = await repository.GetAllAsync();
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
