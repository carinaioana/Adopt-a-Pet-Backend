﻿using FluentValidation;

namespace AdoptPets.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
    {
        public UpdateAnimalCommandValidator()
        {
            RuleFor(p => p.AnimalType)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.AnimalName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.AnimalAge)
                 .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than zero.");
        }
    }
}
