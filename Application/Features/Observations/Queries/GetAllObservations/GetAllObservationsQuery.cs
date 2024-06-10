using MediatR;

namespace AdoptPets.Application.Features.Observations.Queries.GetAllObservations
{
    public class GetAllObservationsQuery : IRequest<GetAllObservationsResponse>
    {
        public Guid AnimalId { get; set; }
    }
}
