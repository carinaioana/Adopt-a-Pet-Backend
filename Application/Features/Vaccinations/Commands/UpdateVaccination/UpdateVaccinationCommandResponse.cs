using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommandResponse : BaseResponse
    {
        public UpdateVaccinationCommandResponse() : base()
        {

        }
        public string Message { get; set; }
    }
    
}
