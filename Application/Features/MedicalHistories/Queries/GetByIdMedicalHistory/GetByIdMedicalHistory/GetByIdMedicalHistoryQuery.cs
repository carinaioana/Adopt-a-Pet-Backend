using AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory;
using MediatR;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetByIdMedicalHistory.GetByIdMedicalHistory
{
    public record GetByIdMedicalHistoryQuery(Guid id) : IRequest<CreateMedicalHistoryDto>;
}
