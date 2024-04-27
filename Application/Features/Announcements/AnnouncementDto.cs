namespace AdoptPets.Application.Features.Announcements
{
    public class AnnouncementDto
    { 
        public Guid AnnouncementId { get; set;}
        public string AnnouncementTitle { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime AnnouncementDate { get; set; }
    }
}
