using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByTitle
{
    public record GetByTitleAnnouncQuery(string title) : IRequest<AnnouncementDto>;

}
