using MediatR;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommand :  IRequest<CreateAnnouncementCommandResponse>
    {
        public string AnnouncementTitle { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public Guid AnimalId { get; private set; }
        public string? AnnouncementDescription { get; private set; }

    }
}
