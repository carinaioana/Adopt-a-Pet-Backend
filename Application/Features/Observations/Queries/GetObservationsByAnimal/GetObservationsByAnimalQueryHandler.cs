using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Observations.Queries.GetObservationsByAnimal
{
    public class GetObservationsByAnimalQueryHandler : IRequestHandler<GetObservationsByAnimalQuery, ObservationDto>
    {
        private readonly IObservationRepository repository;

        public GetObservationsByAnimalQueryHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ObservationDto> Handle(GetObservationsByAnimalQuery request, CancellationToken cancellationToken)
        {
            var deworming = await repository.FindByAnimalId(request.id);
            if (deworming.IsSuccess)
            {
                return new ObservationDto
                {
                    ObservationId = deworming.Value.ObservationId,
                    ObservationDescription = deworming.Value.ObservationDescription,
                    Date = deworming.Value.Date,
                    AnimalId = deworming.Value.AnimalId
                };
            }
            return new ObservationDto();
        }
    }
}
