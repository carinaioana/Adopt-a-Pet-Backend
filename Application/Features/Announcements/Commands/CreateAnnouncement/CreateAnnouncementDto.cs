namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementDto
    {
        public Guid AnnouncementId { get; set; }
        public string? AnnouncementTitle { get; set; }
        public Guid UserId { get; set; }
        public DateTime AnnouncementDate { get; set; }
    }
}
