using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("allowed_emojis")]
    public class AllowedEmoji
    {
        [Column("post_id", Order = 1)]
        public int PostId { get; set; }
        [Column("emoji_id", Order = 2)]
        public int EmojiId { get; set; }
    }
}