using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Observations.Commands.CreateObservation
{
    public class CreateObservationCommandResponse : BaseResponse
    {
        public CreateObservationCommandResponse() : base() { }
        public CreateObservationDto Observation { get; set; } = default!;
    }
}
