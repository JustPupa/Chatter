using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("user_reactions")]
    public class UserReaction
    {
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }
        [Column("post_id", Order = 2)]
        public int PostId { get; set; }
        [Column("emoji_id")]
        public int? EmojiId { get; set; }
    }
}