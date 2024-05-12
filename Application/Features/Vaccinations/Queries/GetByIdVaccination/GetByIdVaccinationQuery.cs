using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;
using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetByIdVaccination
{
    public record GetByIdVaccinationQuery(Guid id) : IRequest<CreateVaccinationDto>;
}
