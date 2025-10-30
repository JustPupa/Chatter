using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("SM_Post_Likes")]
    public class SMPostLike
    {
        [Column("UserId", Order = 1)]
        public int UserId { get; set; }
        [Column("PostId", Order = 2)]
        public int PostId { get; set; }
    }
}
