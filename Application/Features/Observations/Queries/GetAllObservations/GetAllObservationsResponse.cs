using AdoptPets.Application.Features.Observations.Commands.CreateObservation;

namespace AdoptPets.Application.Features.Observations.Queries.GetAllObservations
{
    public class GetAllObservationsResponse
    {
        public List<ObservationDto> Observations { get; set; } = default!;

    }
}
