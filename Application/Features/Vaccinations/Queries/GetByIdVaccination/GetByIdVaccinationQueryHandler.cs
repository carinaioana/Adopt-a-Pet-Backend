using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;
using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetByIdVaccination
{
    public class GetByIdVaccinationQueryHandler : IRequestHandler<GetByIdVaccinationQuery, CreateVaccinationDto>
    {
        private readonly IVaccinationRepository repository;

        public GetByIdVaccinationQueryHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateVaccinationDto> Handle(GetByIdVaccinationQuery request, CancellationToken cancellationToken)
        {
            var vaccination = await repository.FindByIdAsync(request.id);
            if (vaccination.IsSuccess)
            {
                return new CreateVaccinationDto
                {
                    VaccinationId = vaccination.Value.VaccinationId,
                    Date = vaccination.Value.Date,
                    VaccineName = vaccination.Value.VaccineName,
                    AnimalId = vaccination.Value.AnimalId
                };
            }
            return new CreateVaccinationDto();
        }
    }
}
