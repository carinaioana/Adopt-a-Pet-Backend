using AdoptPets.Application.Features.Observations.Commands.CreateObservation;
using MediatR;

namespace AdoptPets.Application.Features.Observations.Queries.GetByIdObservation
{
    public record GetByIdObservationQuery(Guid id) : IRequest<ObservationDto>;
}
