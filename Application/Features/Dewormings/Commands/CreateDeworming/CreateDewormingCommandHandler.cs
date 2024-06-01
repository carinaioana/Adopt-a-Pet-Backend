using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using AdoptPets.Application.Contracts.Interfaces;

namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingCommandHandler : IRequestHandler<CreateDewormingCommand, CreateDewormingCommandResponse>
    {
        private readonly IDewormingRepository repository;
        private readonly ICurrentUserService currentUserService;

        public CreateDewormingCommandHandler(IDewormingRepository repository, ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task<CreateDewormingCommandResponse> Handle(CreateDewormingCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateDewormingCommandResponse();
            var validator = new CreateDewormingCommandValidator(repository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            string userId = currentUserService.GetCurrentUserId();

            if (!validatorResult.IsValid)
            {
                return new CreateDewormingCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var deworming = Deworming.Create(request.AnimalId, request.Date, request.DewormingType);
            if (deworming.IsSuccess)
            {
                deworming.Value.CreatedBy = userId;
                deworming.Value.LastModifiedBy = userId;
                deworming.Value.CreatedDate = DateTime.UtcNow;
                deworming.Value.LastModifiedDate = DateTime.UtcNow;

                var result = repository.AddAsync(deworming.Value);
                return new CreateDewormingCommandResponse
                {
                    Success = true,
                    Deworming = new CreateDewormingDto
                    {
                        DewormingId = deworming.Value.DewormingId,
                        DewormingType = deworming.Value.DewormingType,
                        Date = deworming.Value.Date,
                        AnimalId = deworming.Value.AnimalId,
                    }
                };
            }
            return new CreateDewormingCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { deworming.Error }
            };
        }
    }
}
