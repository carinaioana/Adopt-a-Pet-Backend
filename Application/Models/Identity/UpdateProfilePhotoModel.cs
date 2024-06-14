using Microsoft.AspNetCore.Http;

namespace AdoptPets.Application.Models.Identity
{
    public class UpdateProfilePhotoModel
    {
        public string UserId { get; set; }
        public string? NewProfilePhotoUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
