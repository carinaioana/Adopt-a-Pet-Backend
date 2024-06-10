using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Observations.Queries.GetAllObservations
{
    public class GetAllObservationsQueryHandler : IRequestHandler<GetAllObservationsQuery, GetAllObservationsResponse>
    {
        private readonly IObservationRepository repository;

        public GetAllObservationsQueryHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllObservationsResponse> Handle(GetAllObservationsQuery request, CancellationToken cancellationToken)
        {
            GetAllObservationsResponse response = new();
            var result = await repository.GetAllByAnimalIdAsync(request.AnimalId);

            if (result.IsSuccess)
            {
                response.Observations = result.Value.Select(obs => new ObservationDto
                {
                    ObservationId = obs.ObservationId,
                    Date = obs.Date,
                    ObservationDescription = obs.ObservationDescription,
                    AnimalId = obs.AnimalId,
                  
                }).ToList();
            }
            return response;
        }
    }
}
