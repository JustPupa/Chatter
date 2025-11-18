using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("sm_post_likes")]
    public class SMPostLike
    {
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }
        [Column("post_id", Order = 2)]
        public int PostId { get; set; }
    }
}