using AdoptPets.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
