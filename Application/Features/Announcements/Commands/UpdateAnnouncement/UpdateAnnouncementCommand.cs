using MediatR;

namespace AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommand : IRequest<UpdateAnnouncementCommandResponse>
    {
        public Guid AnnouncementId { get; set; }
        public string AnnouncementTitle { get; set; } = string.Empty;
        public DateTime AnnouncementDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? AnnouncementDescription { get; set; }
    }
}
