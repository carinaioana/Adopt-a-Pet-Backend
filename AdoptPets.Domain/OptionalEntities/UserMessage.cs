using System.ComponentModel.DataAnnotations.Schema;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Domain.OptionalEntities
{
    public class UserMessage
    {
        public Guid UserMessageId { get; set; } // (PK)
        public Guid RecipientUserId { get; set; } // FK pentru utilizatorul care primește
        public Guid MessageId { get; set; } // FK pentru mesaj

        [ForeignKey("RecipientUserId")]
        public User? Recipient { get; set; }

        [ForeignKey("MessageId")]
        public Message? Message { get; set; }
    }
}
