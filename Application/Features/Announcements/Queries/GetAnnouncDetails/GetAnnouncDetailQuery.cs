using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails
{
    public class GetAnnouncDetailQuery : IRequest<GetAnnouncDetailQueryResponse>
    {
        public Guid AnnouncementId { get; set; }
    }
}
