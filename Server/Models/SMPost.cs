using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("sm_posts")]
    public class SMPost : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("post_ref")]
        public int? Post_Ref { get; set; }
        [Column("pub_time")]
        public DateTime? Pub_time { get; set; }
        public User Publisher { get; set; } = new();
        public SMPost? ReferencePost { get; set; } = null;
    }
}