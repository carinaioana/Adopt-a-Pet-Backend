using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Observations.Commands.DeleteObservation
{
    public class DeleteObservationCommandHandler : IRequestHandler<DeleteObservationCommand, DeleteObservationCommandResponse>
    {
        private readonly IObservationRepository repository;

        public DeleteObservationCommandHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteObservationCommandResponse> Handle(DeleteObservationCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.ObservationId);
            if (!result.IsSuccess)
            {
                return new DeleteObservationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteObservationCommandResponse
            {
                Success = true
            };
        }
    }
}
