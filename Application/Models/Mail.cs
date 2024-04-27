using System.Globalization;

namespace AdoptPets.Application.Models
{
    public class Mail
    {
        public String To { get; set; } = string.Empty;
        public String Subject { get; set; } = string.Empty;
        public String Body { get; set; } = string.Empty;
    }
}
