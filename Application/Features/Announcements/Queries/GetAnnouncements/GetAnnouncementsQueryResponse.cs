using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncements
{
    public class GetAnnouncementsQueryResponse : BaseResponse
    {
        public GetAnnouncementsQueryResponse() : base() { }
        public List<AnnouncementDto> Announcements { get; set; } = default!;
    }
}
