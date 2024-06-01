using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Observations.Commands.UpdateObservation
{
    public class UpdateObservationCommand : IRequest<UpdateObservationCommandResponse>
    {
        public Guid ObservationId { get; set; }
        public DateTime Date { get; set; }
        public string ObservationDescription { get; set; } = default!;
        public Guid AnimalId { get; set; }
    }
}
