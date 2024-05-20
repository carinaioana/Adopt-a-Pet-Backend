using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAll
{
    public class GetAllAnnouncementsResponse : BaseResponse
    {
        public GetAllAnnouncementsResponse() : base() { }

        public List<AnnouncementDto> Announcements { get; set; } = default!;

    }
}
