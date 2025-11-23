using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("chat_users")]
    public class ChatUser
    {
        [Column("chat_id", Order = 1)]
        public int ChatId { get; set; }
        [Column("user_id", Order = 2)]
        public int UserId { get; set; }
        public Chat Chat { get; set; } = new();
        public User User { get; set; } = new();
    }
}