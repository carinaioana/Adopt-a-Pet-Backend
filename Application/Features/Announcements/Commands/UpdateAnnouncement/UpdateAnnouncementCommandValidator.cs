using AdoptPets.Application.Persistence;
using FluentValidation;

namespace AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandValidator : AbstractValidator<UpdateAnnouncementCommand>
    {
        private readonly IAnnouncementRepository repository;
        public UpdateAnnouncementCommandValidator(IAnnouncementRepository repository)
        {

            RuleFor(a => a.AnnouncementTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(a => a.AnnouncementDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            this.repository = repository;
        }

        private bool AnnouncementTitleAndDateUnique(UpdateAnnouncementCommand command)
        {
            return !repository.IsAnnouncementTitleAndDateUnique(command.AnnouncementTitle, command.AnnouncementDate).Result;
        }


    }
}
