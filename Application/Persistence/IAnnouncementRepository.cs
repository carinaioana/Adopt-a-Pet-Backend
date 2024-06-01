using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace AdoptPets.Application.Persistence
{
    public interface IAnnouncementRepository : IAsyncRepository<Announcement>
    {
        Task<Result<Announcement>> FindByTitleAsync(string title);
        Task<bool> IsAnnouncementTitleAndDateUnique(string announcementTitle, DateTime announcementDate);
        Task<List<Announcement>> GetAnnouncementsByUserAsync(string userId);
        //Task<(bool Success, string Url)> UploadFileToS3(IFormFile file);



    }
}
