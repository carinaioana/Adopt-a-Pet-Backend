using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
       
        public Task<Announcement> FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<List<Announcement>> GetAnnouncementsByUserAsync(string userId)
        {
            return context.Announcements
            .Where(a => a.CreatedBy != null && a.CreatedBy == userId)
            .ToListAsync(); 
        }
    } 
}
