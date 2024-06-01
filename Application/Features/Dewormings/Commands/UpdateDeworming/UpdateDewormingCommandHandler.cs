using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Dewormings.Commands.UpdateDeworming
{
    public class UpdateDewormingCommandHandler : IRequestHandler<UpdateDewormingCommand, UpdateDewormingCommandResponse>
    {
        private readonly IDewormingRepository repository;

        public UpdateDewormingCommandHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateDewormingCommandResponse> Handle(UpdateDewormingCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDewormingCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateDewormingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var dewormingResult = await repository.FindByIdAsync(request.DewormingId);
            if (!dewormingResult.IsSuccess)
            {
                return new UpdateDewormingCommandResponse
                {
                    Success = false,
                    Message = "Listing not found."
                };
            }

            var deworming = dewormingResult.Value;

            if (request.DewormingType != null)
            {
                deworming.UpdateDewormingType(request.DewormingType);
            }
            if (request.Date != null)
            {
                deworming.UpdateDate(request.Date);
            }
          
            deworming.LastModifiedDate = DateTime.UtcNow;

            var updateResult = await repository.UpdateAsync(deworming);

            if (!updateResult.IsSuccess)
            {
                return new UpdateDewormingCommandResponse
                {
                    Success = false,
                    Message = "Failed to update listing."
                };
            }

            return new UpdateDewormingCommandResponse
            {
                Success = true,
                Message = "Deworming updated successfully."
            };
        }
    }
}
