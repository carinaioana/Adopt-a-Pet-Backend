using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser
{
    public class GetAnnouncementsByUserQueryResponse : BaseResponse
    {
        public GetAnnouncementsByUserQueryResponse() : base() { }
        public List<AnnouncementDto> Announcements { get; set; } = default!;
    }
}
