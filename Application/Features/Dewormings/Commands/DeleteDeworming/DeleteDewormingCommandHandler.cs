using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Dewormings.Commands.DeleteDeworming
{
    public class DeleteDewormingCommandHandler : IRequestHandler<DeleteDewormingCommand, DeleteDewormingCommandResponse>
    {
        private readonly IDewormingRepository repository;

        public DeleteDewormingCommandHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteDewormingCommandResponse> Handle(DeleteDewormingCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.DewormingId);
            if (!result.IsSuccess)
            {
                return new DeleteDewormingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteDewormingCommandResponse
            {
                Success = true
            };
        }
    }
}
