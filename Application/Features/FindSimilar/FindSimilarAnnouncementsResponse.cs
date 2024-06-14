using AdoptPets.Application.Features.Announcements.Queries;
using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.FindSimilar
{
    public class FindSimilarAnnouncementsResponse : BaseResponse
    {
        public FindSimilarAnnouncementsResponse() : base() { }
        public List<AnnouncementDto> Announcements { get; set; } = default!;
    }
}
