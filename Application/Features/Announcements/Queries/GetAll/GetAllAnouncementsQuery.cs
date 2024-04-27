using AdoptPets.Application.Features.Animals.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAll
{
    public class GetAllAnouncementsQuery : IRequest<GetAllAnnouncementsResponse>
    {
    }
}
