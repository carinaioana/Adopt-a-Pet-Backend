using AdoptPets.Application.Persistence;
using FluentValidation;

namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationCommandValidator : AbstractValidator<CreateVaccinationCommand>
    {
        private readonly IVaccinationRepository repository;

        public CreateVaccinationCommandValidator(IVaccinationRepository repository)
        {
            RuleFor(a => a.VaccineName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(a => a)
               .Must(VaccineNameAndDateUnique)
               .WithMessage("An event with the same name and date already exists.");

            this.repository = repository;
            this.repository = repository;
        }
        private bool VaccineNameAndDateUnique(CreateVaccinationCommand command)
        {
            return !repository.IsVaccineNameAndDateUnique(command.VaccineName, command.Date).Result;
        }
    }
}
