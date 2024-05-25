using MediatR;

namespace AdoptPets.Application.Features.Observations.Commands.DeleteObservation
{
    public class DeleteObservationCommandHandler : IRequestHandler<DeleteObservationCommand, DeleteObservationCommandResponse>
    {
        public DeleteObservationCommandHandler()
        {
        }

        public async Task<DeleteObservationCommandResponse> Handle(DeleteObservationCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
