using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingCommandResponse : BaseResponse
    {
        public CreateDewormingCommandResponse() : base() { }
        public CreateDewormingDto Deworming { get; set; } = default!;
    }
}
