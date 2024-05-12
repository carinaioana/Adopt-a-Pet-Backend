using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Observations.Commands.CreateObservation;

namespace AdoptPets.Application.Features.Observations.Queries.GetByIdObservation
{
    public class GetByIdObservationQueryHandler : IRequestHandler<GetByIdObservationQuery, ObservationDto>
    {
        private readonly IObservationRepository repository;

        public GetByIdObservationQueryHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ObservationDto> Handle(GetByIdObservationQuery request, CancellationToken cancellationToken)
        {
            var obs = await repository.FindByIdAsync(request.id);
            if (obs.IsSuccess)
            {
                return new ObservationDto
                {
                    ObservationId = obs.Value.ObservationId,
                    ObservationDescription = obs.Value.ObservationDescription,
                    Date = obs.Value.Date,
                    AnimalId = obs.Value.AnimalId,
                };
            }
            return new ObservationDto();
        }
    }
}
