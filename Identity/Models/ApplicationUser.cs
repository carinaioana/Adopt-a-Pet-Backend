using AdoptPets.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? Name { get; set; }
/*        public string? Password { get;  set; } 
        public string? Username { get;  set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<Animal>? Animals { get; set; }*/


    }
}
