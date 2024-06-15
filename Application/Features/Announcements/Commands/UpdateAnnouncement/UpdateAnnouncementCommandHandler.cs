using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement;

namespace AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, UpdateAnnouncementCommandResponse>
    {
        private readonly IAnnouncementRepository repository;
        private readonly IS3Service s3Service;

        public UpdateAnnouncementCommandHandler(IAnnouncementRepository repository, IS3Service s3Service)
        {
            this.repository = repository;
            this.s3Service = s3Service;
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
            if (request.ImageFile != null)
            {
                var uploadResult = await s3Service.UploadFileAsync(request.ImageFile);
                if (uploadResult.Success)
                {
                    request.ImageUrl = uploadResult.Url;
                }
                else
                {
                    return new UpdateAnnouncementCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { "Image upload failed" }
                    };
                }
                announc.UpdateAnnouncementImageUrl(request.ImageUrl);
            }
            if (request.AnimalType != null)
            {
                announc.AttachAnimalType(request.AnimalType);
            }
            if (request.AnimalBreed != null )
            {
                announc.AttachAnimalBreed(request.AnimalBreed);
            }
            if (request.AnimalGender != null)
            {
                announc.AttachAnimalGender(request.AnimalGender);
            }
            if (request.Location != null)
            {
                announc.AttachLocation(request.Location);
            }

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
