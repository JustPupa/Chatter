using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Chat_Users")]
    public class ChatUser
    {
        [Column("ChatId", Order = 1)]
        public int ChatId { get; set; }
        [Column("UserId", Order = 2)]
        public int UserId { get; set; }
    }
}
