using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IAnnouncementRepository : IAsyncRepository<Announcement>
    {
        Task<Announcement> FindByTitleAsync(string title);
        Task<bool> IsAnnouncementTitleAndDateUnique(string announcementTitle, DateTime announcementDate);
    }
}
