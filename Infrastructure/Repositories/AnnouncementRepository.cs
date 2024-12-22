using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Aspose.Cells.Charts;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace AdoptPets.Infrastructure.Repositories
{
    public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(AdoptPetsContext context) : base(context)
        {

        }
        public Task<bool> IsAnnouncementTitleAndDateUnique(string announcementTitle, DateTime announcementDate)
        {
            var matches = context.Announcements.Any(a => a.AnnouncementTitle.Equals(announcementTitle)
            && a.AnnouncementDate.Date.Equals(announcementDate.Date));
            return Task.FromResult(matches);
        }

        public async Task<Result<Announcement>> FindByTitleAsync(string title)
        {
            try
            {
                var announcement = await context.Announcements
                    .FirstOrDefaultAsync(a => a.AnnouncementTitle.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (announcement == null)
                {
                    return Result<Announcement>.Failure($"Announcement with title '{title}' not found");
                }

                return Result<Announcement>.Success(announcement);
            }
            catch (Exception ex)
            {
                return Result<Announcement>.Failure($"An error occurred while retrieving the announcement: {ex.Message}");
            }
        }

        public Task<List<Announcement>> GetAnnouncementsByUserAsync(string userId)
        {
            return context.Announcements
            .Where(a => a.CreatedBy != null && a.CreatedBy == userId)
            .ToListAsync();
        }

        public async Task<Result<Announcement>> FindByImageAsync(string imageUrl)
        {
            try
            {
                var decodedImageUrl = HttpUtility.UrlDecode(imageUrl).ToLower();
                var announcement = await context.Announcements
                    .FirstOrDefaultAsync(a => a.ImageUrl.ToLower() == decodedImageUrl);


                if (announcement == null)
                {
                    return Result<Announcement>.Failure($"Announcement with image URL '{imageUrl}' not found");
                }

                return Result<Announcement>.Success(announcement);
            }
            catch (Exception ex)
            {
                return Result<Announcement>.Failure($"An error occurred while retrieving the announcement: {ex.Message}");
            }

        }
    }
}

