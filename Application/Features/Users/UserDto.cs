using Microsoft.AspNetCore.Identity;

namespace AdoptPets.Application.Features.Users
{
    public class UserDto
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }

        public string? Location { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.MinValue;
        public string? Description { get; set; }




    }
}
