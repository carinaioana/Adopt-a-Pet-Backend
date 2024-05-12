using MediatR;
using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetByIdDeworming
{
    public class GetByIdDewormingQueryHandler : IRequestHandler<GetByIdDewormingQuery, CreateDewormingDto>
    {
        private readonly IDewormingRepository repository;

        public GetByIdDewormingQueryHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateDewormingDto> Handle(GetByIdDewormingQuery request, CancellationToken cancellationToken)
        {
            var deworming = await repository.FindByIdAsync(request.id);
            if (deworming.IsSuccess)
            {
                return new CreateDewormingDto
                {
                    DewormingId = deworming.Value.DewormingId,
                    DewormingType = deworming.Value.DewormingType,
                    Date = deworming.Value.Date,
                    AnimalId = deworming.Value.AnimalId
                };
            }
            return new CreateDewormingDto();
        }
    }
}
