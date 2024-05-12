using AdoptPets.Application.Persistence;
using FluentValidation;

namespace AdoptPets.Application.Features.Observations.Commands.CreateObservation
{
    public class CreateObservationCommandValidator : AbstractValidator<CreateObservationCommand>
    {
        private readonly IObservationRepository repository;
        public CreateObservationCommandValidator(IObservationRepository repository)
        {
            RuleFor(a => a.ObservationDescription)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(a => a)
               .Must(ObservationDescriptionAndDateUnique)
               .WithMessage("An event with the same name and date already exists.");

            this.repository = repository;
        }
        private bool ObservationDescriptionAndDateUnique(CreateObservationCommand command)
        {
            return !repository.IsObservationDescriptionAndDateUnique(command.ObservationDescription, command.Date).Result;
        }
    }
}
