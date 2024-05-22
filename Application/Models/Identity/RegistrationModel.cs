using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Application.Models.Identity
{
    public class RegistrationModel
    {
       

      /*  [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }*/

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
}
