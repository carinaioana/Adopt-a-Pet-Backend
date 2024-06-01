using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Observations.Commands.DeleteObservation;

namespace AdoptPets.Application.Features.Vaccinations.Commands.DeleteVaccination
{
    public class DeleteVaccinationCommandHandler : IRequestHandler<DeleteVaccinationCommand, DeleteVaccinationCommandResponse>
    {
        private readonly IVaccinationRepository repository;

        public DeleteVaccinationCommandHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteVaccinationCommandResponse> Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.VaccinationId);
            if (!result.IsSuccess)
            {
                return new DeleteVaccinationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteVaccinationCommandResponse
            {
                Success = true
            };
        }
    }
}
