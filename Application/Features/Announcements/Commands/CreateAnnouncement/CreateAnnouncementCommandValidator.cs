using AdoptPets.Application.Persistence;
using FluentValidation;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
    {

        private readonly IAnnouncementRepository repository;
        public CreateAnnouncementCommandValidator(IAnnouncementRepository repository)
        {
            RuleFor(a => a.AnnouncementTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(a => a.AnnouncementDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
             /*RuleFor(x => x.ImageFile.ContentType)
                 .Must(x => x.Equals("image/jpeg") || x.Equals("image/png"))
                 .WithMessage("File must be a JPEG or PNG image.");*/
            
             this.repository = repository;
        }
        private bool AnnouncementTitleAndDateUnique(CreateAnnouncementCommand command)
        {
            return !repository.IsAnnouncementTitleAndDateUnique(command.AnnouncementTitle, command.AnnouncementDate).Result;
        }
    }
}
