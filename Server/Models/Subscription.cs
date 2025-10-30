using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Subscriptions")]
    public class Subscription
    {
        [Column("UserId", Order = 1)]
        public int UserId { get; set; }
        [Column("FollowerId", Order = 2)]
        public int FollowerId { get; set; }
        public DateTime? Date { get; set; }
    }
}
