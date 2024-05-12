using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingCommandHandler : IRequestHandler<CreateDewormingCommand, CreateDewormingCommandResponse>
    {
        private readonly IDewormingRepository repository;

        public CreateDewormingCommandHandler(IDewormingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateDewormingCommandResponse> Handle(CreateDewormingCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateDewormingCommandResponse();
            var validator = new CreateDewormingCommandValidator(repository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
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
