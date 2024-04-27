using AdoptPets.Application.Features.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAll
{
    public class GetAllAnnouncementsResponse
    {
        public List<AnnouncementDto> Announcements { get; set; } = default!;

    }
}
