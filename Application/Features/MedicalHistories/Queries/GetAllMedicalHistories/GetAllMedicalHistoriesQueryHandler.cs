using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetAllMedicalHistories
{
    public class GetAllMedicalHistoriesQueryHandler : IRequestHandler<GetAllMedicalHistoriesQuery, GetAllMedicalHistoriesQueryResponse>
    {
        private readonly IMedicalHistoryRepository repository;

        public GetAllMedicalHistoriesQueryHandler(IMedicalHistoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllMedicalHistoriesQueryResponse> Handle(GetAllMedicalHistoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();
            var medicalHistory = result.Value.Select(e => new MedicalHistoryDto
            {
                MedicalHistoryId = e.MedicalHistoryId,
                UserId = e.UserId,
                AnimalId = e.AnimalId,

            }).ToList(); 

            return new GetAllMedicalHistoriesQueryResponse
            {
                MedicalHistories = medicalHistory,
                Success = true
            };
        }
    }
}
