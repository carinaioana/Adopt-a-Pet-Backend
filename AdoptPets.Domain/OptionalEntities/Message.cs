using System.ComponentModel.DataAnnotations.Schema;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Domain.OptionalEntities
{
    public class Message
    {
        public Guid MessageId { get; set; } // Cheia primară (PK)
        public int SenderUserId { get; set; } // FK pentru utilizatorul care trimite
        public string? Content { get; set; } // Conținutul mesajului
        public DateTime DateMessageSent { get; set; } // Data și ora trimiterii
                                                      // Alte proprietăți ale mesajului

        [ForeignKey("SenderUserId")]
        public User? Sender { get; set; }
    }
}
