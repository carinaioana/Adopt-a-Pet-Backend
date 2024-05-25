using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Announcement : AuditableEntity
    {
        public Announcement(string announcementTitle, DateTime announcementDate)
        {
            AnnouncementId = Guid.NewGuid();
            AnnouncementTitle = announcementTitle;
            AnnouncementDate = announcementDate;
        }
        [Key]
        public Guid AnnouncementId { get; private set; }
        public string AnnouncementTitle { get; private set; } = string.Empty;
        public DateTime AnnouncementDate { get; private set; }
        public string? AnnouncementDescription { get; private set; }
        public string? ImageUrl { get; private set; }
        public static Result<Announcement> Create(string announcementTitle, DateTime announcementDate)
        {
            if (string.IsNullOrWhiteSpace(announcementTitle))
            {
                return Result<Announcement>.Failure("Title is required");
            }
            if (announcementDate == default)
            {
                return Result<Announcement>.Failure("Date is required");
            }
            return Result<Announcement>.Success(new Announcement(announcementTitle, announcementDate));
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
        public void UpdateAnnouncement(string announcementTitle, DateTime announcementDate, string? announcementDescription, string? imageUrl)
        {
            AnnouncementTitle = announcementTitle;
            AnnouncementDate = announcementDate;
            AnnouncementDescription = announcementDescription;
            ImageUrl = imageUrl;
        }
        public void UpdateAnnouncementTitle(string announcementTitle)
        {
            if (!string.IsNullOrEmpty(announcementTitle))
            {
                AnnouncementTitle = announcementTitle;
            }
        }
        public void UpdateAnnouncementDescription(string announcementDescription)
        {
            if (!string.IsNullOrEmpty(announcementDescription))
            {
                AnnouncementDescription = announcementDescription;
            }  
        }
        public void UpdateAnnouncementImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }

        /*public void UpdateAnnouncementDate(DateTime announcementDate)
        {
            if (announcementDate != default)
            {
                announcementDate = announcementDate;
            }
        }*/
    }
}
