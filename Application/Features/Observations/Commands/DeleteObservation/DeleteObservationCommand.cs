using MediatR;

namespace AdoptPets.Application.Features.Observations.Commands.DeleteObservation
{
    public class DeleteObservationCommand : IRequest<DeleteObservationCommandResponse>
    {
        public Guid ObservationId { get; set; }

    }
}
