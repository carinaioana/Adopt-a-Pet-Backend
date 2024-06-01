using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Observations;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetVaccinationsByAnimal
{
    public class GetVaccinationsByAnimalQueryHandler : IRequestHandler<GetVaccinationsByAnimalQuery, VaccinationDto>
    {
        private readonly IVaccinationRepository repository;

        public GetVaccinationsByAnimalQueryHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<VaccinationDto> Handle(GetVaccinationsByAnimalQuery request, CancellationToken cancellationToken)
        {
            var deworming = await repository.FindByAnimalId(request.id);
            if (deworming.IsSuccess)
            {
                return new VaccinationDto
                {
                    VaccinationId = deworming.Value.VaccinationId,
                    VaccineName = deworming.Value.VaccineName,
                    Date = deworming.Value.Date,
                    AnimalId = deworming.Value.AnimalId
                };
            }
            return new VaccinationDto();
        }
    }
}
