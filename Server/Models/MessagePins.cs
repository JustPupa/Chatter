using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("message_pins")]
    public class MessagePin
    {
        [Column("message_id", Order = 1)]
        public int MessageId { get; set; }
        [Column("user_id", Order = 2)]
        public int UserId { get; set; }
    }
}