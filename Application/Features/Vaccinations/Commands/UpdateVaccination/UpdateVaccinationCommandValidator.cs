using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommandValidator : AbstractValidator<UpdateVaccinationCommand>
    {
        public UpdateVaccinationCommandValidator()
        {
            RuleFor(a => a.VaccineName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100)
               .WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
