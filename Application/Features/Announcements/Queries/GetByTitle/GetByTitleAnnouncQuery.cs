using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByTitle
{
    public record GetByTitleAnnouncQuery(string title) : IRequest<AnnouncementDto>;

}
