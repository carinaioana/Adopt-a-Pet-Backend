using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.Users
{
    public class UserDto
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; } 
        public Boolean? IsAdmin { get; set; }
        public string? Location { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneNumberConfirmed { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Boolean? TwoFactorEnabled { get; set; }
        public int? AccessFailedCount { get; set; }



    }
}
