using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByImageUrl
{
    public class GetByImageUrlQueryResponse : BaseResponse
    {
        public GetByImageUrlQueryResponse() : base()
        {
        }
        public AnnouncementDto Announcement { get; set; } = default!;
    }
}
