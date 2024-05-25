using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IAnnouncementRepository : IAsyncRepository<Announcement>
    {
        Task<Result<Announcement>> FindByTitleAsync(string title);
        Task<bool> IsAnnouncementTitleAndDateUnique(string announcementTitle, DateTime announcementDate);
        Task<List<Announcement>> GetAnnouncementsByUserAsync(string userId);
       

    }
}
