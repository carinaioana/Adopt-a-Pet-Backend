using AdoptPets.Application.Models;
using MediatR;

namespace AdoptPets.Application.Features.Animals.Queries.GetById
{
    public record GetByIdAnimalQuery(Guid id) : IRequest<AnimalDto>;
}
