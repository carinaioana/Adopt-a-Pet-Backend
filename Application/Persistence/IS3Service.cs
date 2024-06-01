using Microsoft.AspNetCore.Http;

namespace AdoptPets.Application.Persistence
{
    public interface IS3Service
    {
        Task<(bool Success, string Url)> UploadFileAsync(IFormFile file);
    }
}
