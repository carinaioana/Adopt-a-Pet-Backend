using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.DeleteMedicalHistory
{
    public class DeleteMedicalHistoryCommandHandler : IRequestHandler<DeleteMedicalHistoryCommand, DeleteMedicalHistoryCommandResponse>
    {
        private readonly IMedicalHistoryRepository repository;

        public DeleteMedicalHistoryCommandHandler(IMedicalHistoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteMedicalHistoryCommandResponse> Handle(DeleteMedicalHistoryCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.MedicalHistoryId);
            if (!result.IsSuccess)
            {
                return new DeleteMedicalHistoryCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteMedicalHistoryCommandResponse
            {
                Success = true
            };
        }
    }
}
