using MediatR;

namespace AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement
{
    public class DeleteAnnouncementCommand : IRequest<DeleteAnnouncementCommandResponse>
    {
        public Guid AnnouncementId { get; set; }
    }
}
