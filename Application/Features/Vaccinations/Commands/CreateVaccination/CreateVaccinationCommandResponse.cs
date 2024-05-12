using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationCommandResponse : BaseResponse
    {
        public CreateVaccinationCommandResponse() : base() { }
        public VaccinationDto Vaccination { get; set; } = default!;

    }
}
