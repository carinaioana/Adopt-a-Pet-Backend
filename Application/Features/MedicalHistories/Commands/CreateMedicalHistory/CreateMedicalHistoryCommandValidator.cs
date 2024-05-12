using FluentValidation;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryCommandValidator : AbstractValidator<CreateMedicalHistoryCommand>
    {
        public CreateMedicalHistoryCommandValidator()
        {
            RuleFor(a => a.AnimalId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }

}
