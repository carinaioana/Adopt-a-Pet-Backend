using MediatR;

namespace AdoptPets.Application.Features.Observations.Queries.GetObservationsByAnimal
{
    public record GetObservationsByAnimalQuery(Guid id) : IRequest<ObservationDto>;
}
