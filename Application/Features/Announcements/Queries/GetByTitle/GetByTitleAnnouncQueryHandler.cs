using AdoptPets.Application.Features.Announcements.Queries.GetById;
using AdoptPets.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByTitle
{
    public class GetByTitleAnnouncQueryHandler
    {
        private readonly IAnnouncementRepository repository;
        public GetByTitleAnnouncQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }
    }
}
