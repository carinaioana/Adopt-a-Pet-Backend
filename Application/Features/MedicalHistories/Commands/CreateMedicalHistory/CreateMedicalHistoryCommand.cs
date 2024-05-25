using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory
{
    public class CreateMedicalHistoryCommand : IRequest<CreateMedicalHistoryCommandResponse>
    {
        public Guid AnimalId { get; set; }
        public String UserId { get; set; } = String.Empty;

    /*    public List<Deworming>? Dewormings { get; set; }
        public List<Vaccination>? Vaccinations { get; set; }
        public List<Observation>? Observations { get; set; }*/
    }
}
