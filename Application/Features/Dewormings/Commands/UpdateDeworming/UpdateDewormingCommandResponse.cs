using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Dewormings.Commands.UpdateDeworming
{
    public class UpdateDewormingCommandResponse : BaseResponse
    {
        public UpdateDewormingCommandResponse() : base()
        {
            
        }
        public string Message { get; set; }
    }
}
