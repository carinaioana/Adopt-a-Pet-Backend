using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommand : IRequest<UpdateVaccinationCommandResponse>
    {
        public Guid VaccinationId { get; set; }
        public DateTime Date { get; set; }
        public string VaccineName { get; set; } = default!;
        public Guid AnimalId { get; set; }
    }
}
