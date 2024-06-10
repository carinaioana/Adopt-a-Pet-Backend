namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class AnnouncementData
    {
        public string AnnouncementTitle { get; set; } = string.Empty;
        public DateTime AnnouncementDate { get; set; }
        public string? AnnouncementDescription { get; set; }
    }
}
