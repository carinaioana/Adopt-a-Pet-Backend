using BackupMonitoring.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Announcement : AuditableEntity
    {
        public Announcement(string announcementTitle, Guid userId, DateTime announcementDate)
        {
            AnnouncementId = Guid.NewGuid();
            AnnouncementTitle = announcementTitle;
            UserId = userId;
            Date = announcementDate;
        }
        [Key]
        public Guid AnnouncementId { get; private set; }
        public string AnnouncementTitle { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }
        public Guid AnimalId { get; private set; }
        public DateTime Date { get; private set; }
        public string? AnnouncementDescription { get; private set; }
        /*public int UserId { get; set; } // Foreign Key

        [ForeignKey("UserId")]
        public required User User { get; set; }*/

        /*public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public required Animal Animal { get; set; }*/

        public DateTime AnnouncementDate { get; private set; }

        public string? ImageUrl { get; private set; }
        public static Result<Announcement> Create(string announcementTitle, Guid userId, DateTime announcementDate)
        {
            if (string.IsNullOrWhiteSpace(announcementTitle))
            {
                return Result<Announcement>.Failure("Title is required");
            }
            if (userId == Guid.Empty)
            {
                return Result<Announcement>.Failure("User is required");
            }
             if (announcementDate == default(DateTime))
            {
                return Result<Announcement>.Failure("Date is required");
            }
            return Result<Announcement>.Success(new Announcement(announcementTitle, userId, announcementDate));
        }

        public void AttachAnimal(Guid animalId)
        {
            if (animalId != Guid.Empty)
            {
                AnimalId = animalId;
            }
        }
        public void AttachDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                AnnouncementDescription = description;
            }
        }

        public void AttachImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }
    }
}
