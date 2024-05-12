using AdoptPets.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails
{
    public class GetAnnouncDetailQueryResponse : BaseResponse
    {
        public GetAnnouncDetailQueryResponse(): base() { }
        public AnnouncementDto Announcement { get; set; } = default!;
    }
}
