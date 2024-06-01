using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Dewormings.Commands.UpdateDeworming
{
    public class UpdateDewormingCommand : IRequest<UpdateDewormingCommandResponse>
    {
        public Guid DewormingId { get; set; }
        public DateTime Date { get; set; }
        public string DewormingType { get; set; } = default!;
        public Guid AnimalId { get; set; }
    }
}
