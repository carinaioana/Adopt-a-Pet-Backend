using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Commands.DeleteVaccination
{
    public class DeleteVaccinationCommand : IRequest<DeleteVaccinationCommandResponse>
    {
        public Guid VaccinationId { get; set; }

    }
}
