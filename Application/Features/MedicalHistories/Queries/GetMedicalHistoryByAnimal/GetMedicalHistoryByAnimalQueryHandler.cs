using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Dewormings;
using AdoptPets.Application.Features.Vaccinations;
using AdoptPets.Application.Features.Observations;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetMedicalHistoryByAnimal
{
    public class GetMedicalHistoryByAnimalQueryHandler : IRequestHandler<GetMedicalHistoryByAnimalQuery, MedicalHistoryDto>
    {
        private readonly IMedicalHistoryRepository repository;

        public GetMedicalHistoryByAnimalQueryHandler(IMedicalHistoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MedicalHistoryDto> Handle(GetMedicalHistoryByAnimalQuery request, CancellationToken cancellationToken)
        {
            var deworming = await repository.FindByAnimalId(request.id);
            if (deworming.IsSuccess)
            {
                return new MedicalHistoryDto
                {
                    MedicalHistoryId = deworming.Value.MedicalHistoryId,
                    UserId = deworming.Value.UserId,
                    AnimalId = deworming.Value.AnimalId,
                  
                };
            }
            return new MedicalHistoryDto();
        }
    }
}
