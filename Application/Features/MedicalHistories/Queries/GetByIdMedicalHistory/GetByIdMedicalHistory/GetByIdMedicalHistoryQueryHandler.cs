using MediatR;
using AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Models.Identity;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetByIdMedicalHistory.GetByIdMedicalHistory
{
    public class GetByIdMedicalHistoryQueryHandler : IRequestHandler<GetByIdMedicalHistoryQuery, MedicalHistoryDto>
    {
        private readonly IMedicalHistoryRepository repository;

        public GetByIdMedicalHistoryQueryHandler(IMedicalHistoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MedicalHistoryDto> Handle(GetByIdMedicalHistoryQuery request, CancellationToken cancellationToken)
        {
            var medicalHistory = await repository.FindByIdAsync(request.id);
            if (medicalHistory.IsSuccess)
            {
                return new MedicalHistoryDto
                {
                    MedicalHistoryId = medicalHistory.Value.MedicalHistoryId,
                    AnimalId = medicalHistory.Value.AnimalId,
                    UserId = medicalHistory.Value.UserId,
                    
                };
            }
            return new MedicalHistoryDto();
        }
    }
}
