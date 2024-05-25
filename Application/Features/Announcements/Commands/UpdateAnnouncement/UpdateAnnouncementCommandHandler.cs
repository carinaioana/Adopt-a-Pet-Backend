using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, UpdateAnnouncementCommandResponse>
    {
        private readonly IAnnouncementRepository repository;

        public UpdateAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateAnnouncementCommandResponse> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAnnouncementCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateAnnouncementCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var announcResult = await repository.FindByIdAsync(request.AnnouncementId);
            if (!announcResult.IsSuccess)
            {
                return new UpdateAnnouncementCommandResponse
                {
                    Success = false,
                    Message = "Listing not found."
                };
            }

            var announc = announcResult.Value;

            if (request.AnnouncementTitle != null)
            {
                announc.UpdateAnnouncementTitle(request.AnnouncementTitle);
            }
            if (request.AnnouncementDescription != null)
            {
                announc.UpdateAnnouncementDescription(request.AnnouncementDescription);
            }
            if(request.ImageUrl != null)
            {
                announc.UpdateAnnouncementImageUrl(request.ImageUrl);
            }
           /* if (request.AnnouncementDate != null)
            {
                announc.UpdateAnnouncementDate(request.AnnouncementDate);
            }*/
            announc.LastModifiedDate = DateTime.UtcNow;

            var updateResult = await repository.UpdateAsync(announc);

            if (!updateResult.IsSuccess)
            {
                return new UpdateAnnouncementCommandResponse
                {
                    Success = false,
                    Message = "Failed to update listing."
                };
            }

            return new UpdateAnnouncementCommandResponse
            {
                Success = true,
                Message = "Listing updated successfully."
            };
        }
    }
}
