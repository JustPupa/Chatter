using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("User_Reactions")]
    public class UserReaction
    {
        [Column("UserId", Order = 1)]
        public int UserId { get; set; }
        [Column("PostId", Order = 2)]
        public int PostId { get; set; }
        public int? EmojiId { get; set; }
    }
}
