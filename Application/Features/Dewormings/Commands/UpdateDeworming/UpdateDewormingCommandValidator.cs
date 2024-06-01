using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Dewormings.Commands.UpdateDeworming
{
    public class UpdateDewormingCommandValidator : AbstractValidator<UpdateDewormingCommand>
    {
        public UpdateDewormingCommandValidator()
        {

            RuleFor(a => a.DewormingType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
