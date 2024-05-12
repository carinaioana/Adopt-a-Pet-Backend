using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryCommandResponse : BaseResponse
    {
        public CreateMedicalHistoryCommandResponse() : base() { }
        public CreateMedicalHistoryDto MedicalHistoryDto { get; set; } = default!;
    }

}
