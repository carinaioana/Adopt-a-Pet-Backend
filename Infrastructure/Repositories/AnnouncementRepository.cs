using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
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

        Task<Announcement> IAnnouncementRepository.FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        /*public override async Task<Result<Announcement>> FindByIdAsync(Guid id)
        {
            var result = await context.Announcements.Include(e => e.Animal).FirstOrDefaultAsync(e => e.AnnouncementId.Equals(id))!;
            if (result == null)
            {
                return Result<Announcement>.Failure($"Entity with id {id} not found");
            }
            return Result<Announcement>.Success(result);
        }
        public override async Task<Result<IReadOnlyList<Announcement>>> GetAllAsync()
        {
            var result = await context.Announcements.Include(e =>e.Animal).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<Announcement>>.Success(result);
        }*/
    } 
}
