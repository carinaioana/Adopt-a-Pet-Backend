using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Observations.Commands.DeleteObservation
{
    public class DeleteObservationCommand : IRequest<DeleteObservationCommandResponse>
    {
    }
}
