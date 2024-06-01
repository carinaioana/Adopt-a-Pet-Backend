using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement;
using AdoptPets.Application.Features.Dewormings.Commands.UpdateDeworming;

namespace AdoptPets.Application.Features.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommandHandler : IRequestHandler<UpdateVaccinationCommand, UpdateVaccinationCommandResponse>
    {
        private readonly IVaccinationRepository repository;

        public UpdateVaccinationCommandHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateVaccinationCommandResponse> Handle(UpdateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateVaccinationCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateVaccinationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var vaccinationResult = await repository.FindByIdAsync(request.VaccinationId);
            if (!vaccinationResult.IsSuccess)
            {
                return new UpdateVaccinationCommandResponse
                {
                    Success = false,
                    Message = "Listing not found."
                };
            }

            var deworming = vaccinationResult.Value;

            if (request.VaccineName != null)
            {
                deworming.UpdateVaccineName(request.VaccineName);
            }
            if (request.Date != null)
            {
                deworming.UpdateDate(request.Date);
            }

            deworming.LastModifiedDate = DateTime.UtcNow;

            var updateResult = await repository.UpdateAsync(deworming);

            if (!updateResult.IsSuccess)
            {
                return new UpdateVaccinationCommandResponse
                {
                    Success = false,
                    Message = "Failed to update listing."
                };
            }

            return new UpdateVaccinationCommandResponse
            {
                Success = true,
                Message = "Vaccination updated successfully."
            };
        }
    }
}
