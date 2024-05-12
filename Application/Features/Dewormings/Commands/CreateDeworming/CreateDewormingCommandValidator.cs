using AdoptPets.Application.Persistence;
using FluentValidation;

namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingCommandValidator : AbstractValidator<CreateDewormingCommand>
    {
        private readonly IDewormingRepository repository;

        public CreateDewormingCommandValidator(IDewormingRepository repository)
        {
            RuleFor(a => a.DewormingType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(a => a)
               .Must(DewormingTypeAndDateUnique)
               .WithMessage("An event with the same name and date already exists.");

            this.repository = repository;
        }
        private bool DewormingTypeAndDateUnique(CreateDewormingCommand command)
        {
            return !repository.IsDewormingTypeAndDateUnique(command.DewormingType, command.Date).Result;
        }
    }
}
