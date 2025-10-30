using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Message_Pins")]
    public class MessagePin
    {
        [Column("MessageId", Order = 1)]
        public int MessageId { get; set; }
        [Column("UserId", Order = 2)]
        public int UserId { get; set; }
    }
}