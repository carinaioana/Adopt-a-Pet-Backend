using MediatR;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetMedicalHistoryByAnimal
{
    public record GetMedicalHistoryByAnimalQuery(Guid id) : IRequest<MedicalHistoryDto>;
}
