using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using AdoptPets.Application.Contracts.Interfaces;

namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationCommandHandler : IRequestHandler<CreateVaccinationCommand, CreateVaccinationCommandResponse>
    {
        private readonly IVaccinationRepository repository;
        private readonly ICurrentUserService currentUserService;

        public CreateVaccinationCommandHandler(IVaccinationRepository repository, ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task<CreateVaccinationCommandResponse> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateVaccinationCommandResponse();
            var validator = new CreateVaccinationCommandValidator(repository);

            string userId = currentUserService.GetCurrentUserId();

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

                vaccination.Value.CreatedBy = userId;
                vaccination.Value.LastModifiedBy = userId;
                vaccination.Value.CreatedDate = DateTime.UtcNow;
                vaccination.Value.LastModifiedDate = DateTime.UtcNow;

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
