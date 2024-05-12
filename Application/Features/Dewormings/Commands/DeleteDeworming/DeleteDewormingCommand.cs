using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Commands.DeleteDeworming
{
    public class DeleteDewormingCommand : IRequest<DeleteDewormingCommandResponse>
    {
        public Guid DewormingId { get; set; }
    }
}
