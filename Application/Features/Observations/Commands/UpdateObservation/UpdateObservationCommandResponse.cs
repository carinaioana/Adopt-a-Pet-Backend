using AdoptPets.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Observations.Commands.UpdateObservation
{
    public class UpdateObservationCommandResponse : BaseResponse
    {
        public UpdateObservationCommandResponse() : base()
        {
            
        }
        public string Message { get; set; }
    }
}
