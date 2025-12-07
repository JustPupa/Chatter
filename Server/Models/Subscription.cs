using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("subscriptions")]
    public class Subscription : IEntity
    {
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }
        [Column("follower_id", Order = 2)]
        public int FollowerId { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
    }
}