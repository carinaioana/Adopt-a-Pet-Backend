using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Features.Announcements.Queries
{
    public class AnnouncementDto
    {
        public Guid AnnouncementId { get; set; }
        public string AnnouncementTitle { get; set; } = default!;
        public string? AnnouncementDescription { get; set; }
        public DateTime AnnouncementDate { get; set; }

        public string? AnimalType { get; set; }
        public string? AnimalBreed { get; set; }
        public string? AnimalGender { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }
        public string? CreatedBy { get; set; }
    }
}
