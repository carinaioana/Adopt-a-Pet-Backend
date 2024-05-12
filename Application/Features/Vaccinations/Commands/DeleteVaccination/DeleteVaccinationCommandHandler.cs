using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Vaccinations.Commands.DeleteVaccination
{
    public class DeleteVaccinationCommandHandler : IRequestHandler<DeleteVaccinationCommand, DeleteVaccinationCommandResponse>
    {
        public DeleteVaccinationCommandHandler()
        {
        }

        public async Task<DeleteVaccinationCommandResponse> Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
