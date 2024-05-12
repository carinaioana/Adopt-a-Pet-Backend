using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationCommand : IRequest<CreateVaccinationCommandResponse>
    {
        public DateTime Date { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }

    }
}
