using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryCommandHandler : IRequestHandler<CreateMedicalHistoryCommand, CreateMedicalHistoryCommandResponse>
    {
        private readonly IMedicalHistoryRepository repository;

        public CreateMedicalHistoryCommandHandler(IMedicalHistoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateMedicalHistoryCommandResponse> Handle(CreateMedicalHistoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            /* var response = new CreateMedicalHistoryCommandResponse();

             var validator = new CreateMedicalHistoryCommandValidator();
             var validatorResult = await validator.ValidateAsync(request, cancellationToken);
             if (!validatorResult.IsValid)
             { 
                 return new CreateMedicalHistoryCommandResponse
                 {
                     Success = false,
                     ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                 };
             }

             var medicalHistory = MedicalHistory.Create(request.AnimalId, request.UserId);
             if (medicalHistory.IsSuccess)
             {
                 var result = repository.AddAsync(medicalHistory.Value);
                 return new CreateMedicalHistoryCommandResponse
                 {
                     Success = true,
                     MedicalHistoryDto = new CreateMedicalHistoryDto
                     {
                         MedicalHistoryId = medicalHistory.Value.MedicalHistoryId,
                         AnimalId = medicalHistory.Value.AnimalId,
                         UserId = medicalHistory.Value.UserId
                     }
                 };
             }
             return new CreateMedicalHistoryCommandResponse
             {
                 Success = false,
                 ValidationsErrors = new List<string> { medicalHistory.Error }
             };*/
        }
    }
}
