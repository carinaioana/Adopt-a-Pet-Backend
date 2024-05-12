using AdoptPets.Application.Features.MedicalHistories;
using MediatR;

namespace AdoptPets.Application.Features.Observations.Commands.CreateObservation
{
    public class CreateObservationCommand : IRequest<CreateObservationCommandResponse>
    {
        public DateTime Date { get; set; }
        public string ObservationDescription { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }
    }
}
