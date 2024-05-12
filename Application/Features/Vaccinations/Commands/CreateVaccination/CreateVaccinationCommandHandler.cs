using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationCommandHandler : IRequestHandler<CreateVaccinationCommand, CreateVaccinationCommandResponse>
    {
        private readonly IVaccinationRepository repository;

        public CreateVaccinationCommandHandler(IVaccinationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateVaccinationCommandResponse> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateVaccinationCommandResponse();
            var validator = new CreateVaccinationCommandValidator(repository);

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new CreateVaccinationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var vaccination = Vaccination.Create(request.AnimalId, request.Date, request.VaccineName);
            if (vaccination.IsSuccess)
            {
                var result = repository.AddAsync(vaccination.Value);
                return new CreateVaccinationCommandResponse
                {
                    Success = true,
                    Vaccination = new VaccinationDto
                    {
                        VaccinationId = vaccination.Value.VaccinationId,
                        VaccineName = vaccination.Value.VaccineName,
                        Date = vaccination.Value.Date,
                        AnimalId = vaccination.Value.AnimalId
                    }
                };
            }
            return new CreateVaccinationCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { vaccination.Error }
            };
        }
    }
}
