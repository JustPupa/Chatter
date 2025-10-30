using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Allowed_Emojis")]
    public class AllowedEmoji
    {
        [Column("PostId", Order = 1)]
        public int PostId { get; set; }
        [Column("EmojiId", Order = 2)]
        public int EmojiId { get; set; }
    }
}