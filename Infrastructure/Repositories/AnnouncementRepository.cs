using AdoptPets.Application.Features.Announcements;
using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
using AdoptPets.Infrastructure;
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

        Task<string> IAnnouncementRepository.FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}
