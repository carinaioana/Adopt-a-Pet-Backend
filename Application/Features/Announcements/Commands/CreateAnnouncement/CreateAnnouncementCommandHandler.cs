using AdoptPets.Application.Contracts;
using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Models;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, CreateAnnouncementCommandResponse>
    {
        private readonly IS3Service s3Service;
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateAnnouncementCommandHandler> logger;
        private readonly ICurrentUserService currentUserService;

        public CreateAnnouncementCommandHandler(
            IS3Service s3Service,
            IAnnouncementRepository repository,
            IEmailService emailService,
            ILogger<CreateAnnouncementCommandHandler> logger,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService)
        {
            this.s3Service = s3Service;
            this.announcementRepository = repository;
            this.emailService = emailService;
            this.logger = logger;
            this.currentUserService = currentUserService;
        }

        public async Task<CreateAnnouncementCommandResponse> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAnnouncementCommandValidator(announcementRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            string userId = currentUserService.GetCurrentUserId();



            if (!validatorResult.IsValid)
            {
                return new CreateAnnouncementCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }
            var announcement = Announcement.Create(request.AnnouncementTitle, request.AnnouncementDate);

            if (announcement.IsSuccess)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                announcement.Value.AttachDescription(request.AnnouncementDescription);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                if (request.ImageFile != null)
                {
                    var uploadResult = await s3Service.UploadFileAsync(request.ImageFile);
                    if (uploadResult.Success)
                    {
                        announcement.Value.AttachImageUrl(request.ImageUrl);
                        request.ImageUrl = uploadResult.Url;
                    }
                    else
                    {
                        return new CreateAnnouncementCommandResponse()
                        {
                            Success = false,
                            ValidationsErrors = new List<string> { "Image upload failed" }
                        };
                    }
                }
                
#pragma warning restore CS8604 // Possible null reference argument.

                    announcement.Value.CreatedBy = userId;
                    announcement.Value.LastModifiedBy = userId;
                    announcement.Value.CreatedDate = DateTime.UtcNow;
                    announcement.Value.LastModifiedDate = DateTime.UtcNow;


                    var result = announcementRepository.AddAsync(announcement.Value);

                    var email = new Mail
                    {
                        To = "carinasrb@yahoo.com",
                        Subject = "New Announcement created",
                        Body = $"A new announcement with name: {announcement.Value.AnnouncementTitle} and date: {announcement.Value.AnnouncementDate} has been created."
                    };
                    try
                    {
                        await emailService.SendEmailAsync(email);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Email sending failed");
                        return new CreateAnnouncementCommandResponse
                        {
                            Success = false,
                            ValidationsErrors = new List<string> { "Email sending failed" }
                        };
                    }
                    return new CreateAnnouncementCommandResponse
                    {
                        Success = true,
                        Announcement = new CreateAnnouncementDto
                        {
                            AnnouncementId = announcement.Value.AnnouncementId,
                            AnnouncementTitle = announcement.Value.AnnouncementTitle,
                            AnnouncementDate = announcement.Value.AnnouncementDate,
                            AnnouncementDescription = announcement.Value.AnnouncementDescription,
                            ImageUrl = announcement.Value.ImageUrl,


                        }
                    };
                }
                return new CreateAnnouncementCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { announcement.Error }
                };
            }
        }
}

