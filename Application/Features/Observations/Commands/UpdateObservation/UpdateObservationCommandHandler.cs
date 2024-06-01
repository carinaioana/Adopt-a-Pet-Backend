using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Observations.Commands.UpdateObservation
{
    public class UpdateObservationCommandHandler : IRequestHandler<UpdateObservationCommand, UpdateObservationCommandResponse>
    {
        private readonly IObservationRepository repository;

        public UpdateObservationCommandHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateObservationCommandResponse> Handle(UpdateObservationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateObservationCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateObservationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var observationResult = await repository.FindByIdAsync(request.ObservationId);
            if (!observationResult.IsSuccess)
            {
                return new UpdateObservationCommandResponse
                {
                    Success = false,
                    Message = "Listing not found."
                };
            }

            var deworming = observationResult.Value;

            if (request.ObservationDescription != null)
            {
                deworming.UpdateObservationDescription(request.ObservationDescription);
            }
            if (request.Date != null)
            {
                deworming.UpdateDate(request.Date);
            }

            deworming.LastModifiedDate = DateTime.UtcNow;

            var updateResult = await repository.UpdateAsync(deworming);

            if (!updateResult.IsSuccess)
            {
                return new UpdateObservationCommandResponse
                {
                    Success = false,
                    Message = "Failed to update listing."
                };
            }

            return new UpdateObservationCommandResponse
            {
                Success = true,
                Message = "Observation updated successfully."
            };
        }
    }
}
