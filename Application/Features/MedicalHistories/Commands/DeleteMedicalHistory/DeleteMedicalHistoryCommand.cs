using MediatR;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.DeleteMedicalHistory
{
    public class DeleteMedicalHistoryCommand : IRequest<DeleteMedicalHistoryCommandResponse>
    {
        public Guid MedicalHistoryId { get; set; }
    }
}
