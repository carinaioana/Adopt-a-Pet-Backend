using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Observations.Commands.UpdateObservation
{
    public class UpdateObservationCommandValidator : AbstractValidator<UpdateObservationCommand>
    {
        public UpdateObservationCommandValidator()
        {
            RuleFor(a => a.ObservationDescription)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100)
               .WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
