using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetVaccinationsByAnimal
{
    public record GetVaccinationsByAnimalQuery(Guid id) : IRequest<VaccinationDto>;
    
}
