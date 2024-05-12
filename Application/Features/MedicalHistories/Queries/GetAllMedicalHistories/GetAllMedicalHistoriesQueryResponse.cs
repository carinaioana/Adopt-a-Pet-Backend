using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetAllMedicalHistories
{
    public class GetAllMedicalHistoriesQueryResponse : BaseResponse
    {
        public GetAllMedicalHistoriesQueryResponse() : base() { }

        public List<MedicalHistoryDto> MedicalHistories { get; set; } = default!;

    }
}
