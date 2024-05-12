using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Vaccinations.Commands.DeleteVaccination
{
    public class DeleteVaccinationCommand : IRequest<DeleteVaccinationCommandResponse>
    {
    }
}
