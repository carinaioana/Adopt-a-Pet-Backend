using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.Observations.Commands.CreateObservation
{
    public class CreateObservationCommandHandler : IRequestHandler<CreateObservationCommand, CreateObservationCommandResponse>
    {
        private readonly IObservationRepository repository;

        public CreateObservationCommandHandler(IObservationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateObservationCommandResponse> Handle(CreateObservationCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateObservationCommandResponse();
            var validator = new CreateObservationCommandValidator(repository);

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new CreateObservationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var observation = Observation.Create(request.AnimalId, request.Date, request.ObservationDescription);
            if (observation.IsSuccess)
            {
                var result = repository.AddAsync(observation.Value);
                return new CreateObservationCommandResponse
                {
                    Success = true,
                    Observation = new CreateObservationDto
                    {
                        ObservationId = observation.Value.ObservationId,
                        AnimalId = observation.Value.AnimalId,
                        Date = observation.Value.Date,
                        ObservationDescription = observation.Value.ObservationDescription,
                    }
                };
            }

            return new CreateObservationCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { observation.Error }
            };
        }
    }
}
