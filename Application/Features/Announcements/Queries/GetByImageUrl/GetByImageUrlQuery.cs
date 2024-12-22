using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser;
using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByImageUrl
{
    public class GetByImageUrlQuery : IRequest<GetByImageUrlQueryResponse>
    {
        public string? ImageUrl { get; set; }
    }
}
