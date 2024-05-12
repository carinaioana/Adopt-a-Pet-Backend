using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Observations.Commands.DeleteObservation
{
    public class DeleteObservationCommandHandler : IRequestHandler<DeleteObservationCommand, DeleteObservationCommandResponse>
    {
        public DeleteObservationCommandHandler()
        {
        }

        public async Task<DeleteObservationCommandResponse> Handle(DeleteObservationCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
