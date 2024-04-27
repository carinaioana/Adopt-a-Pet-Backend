using AdoptPets.Application.Contracts;
using AdoptPets.Application.Models;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, CreateAnnouncementCommandResponse>
    {
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateAnnouncementCommandHandler> logger;

        public CreateAnnouncementCommandHandler(IAnnouncementRepository repository, IEmailService emailService, ILogger<CreateAnnouncementCommandHandler> logger)
        {
            this.announcementRepository = repository;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<CreateAnnouncementCommandResponse> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAnnouncementCommandValidator(announcementRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateAnnouncementCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }
            var announcement = Announcement.Create(request.AnnouncementTitle, request.UserId, request.AnnouncementDate);
            if (announcement.IsSuccess)
            {
                announcement.Value.AttachAnimal(request.AnimalId);
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
                return new CreateAnnouncementCommandResponse()
                {
                    Success = true,
                    Announcement = new CreateAnnouncementDto
                    {
                        AnnouncementId = announcement.Value.AnnouncementId,
                        AnnouncementTitle = announcement.Value.AnnouncementTitle,
                        AnnouncementDate = announcement.Value.AnnouncementDate,
                        UserId = announcement.Value.UserId,
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

