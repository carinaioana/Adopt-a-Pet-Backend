using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetAllVaccinations
{
    public class GetAllVaccinationsQueryHandler : IRequestHandler<GetAllVaccinationsQuery, GetAllVaccinationsResponse>
    {
        private readonly IVaccinationRepository repository;

        public GetAllVaccinationsQueryHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllVaccinationsResponse> Handle(GetAllVaccinationsQuery request, CancellationToken cancellationToken)
        {
            GetAllVaccinationsResponse response = new();
            var result = await repository.GetAllByAnimalIdAsync(request.AnimalId);
            if (result.IsSuccess)
            {
                response.Vaccinations = result.Value.Select(vaccinations => new VaccinationDto
                {
                    VaccinationId = vaccinations.VaccinationId,
                    Date = vaccinations.Date,
                    VaccineName = vaccinations.VaccineName,
                    AnimalId = vaccinations.AnimalId,
                }).ToList();
            }
            return response;
        }
    }
}
