using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("messages")]
    public class Message
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("chat_id")]
        public int ChatId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("content")]
        public required string Content { get; set; }
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }
        [Column("message_ref_id")]
        public int? MessageReferenceId { get; set; }
        [Column("is_public")]
        public bool? IsPublic { get; set; }
        [Column("media_id")]
        public int? MediaId { get; set; }
    }
}