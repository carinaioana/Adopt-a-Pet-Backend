using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
