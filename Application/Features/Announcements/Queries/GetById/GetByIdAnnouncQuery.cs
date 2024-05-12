using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetById
{
    public record GetByIdAnnouncQuery(Guid id) : IRequest<AnnouncementDto>;

}
