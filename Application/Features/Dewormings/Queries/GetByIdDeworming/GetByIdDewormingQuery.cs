using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetByIdDeworming
{
    public record GetByIdDewormingQuery(Guid id) : IRequest<CreateDewormingDto>;
 
}
