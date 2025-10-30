using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string MesContent { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? MessageReferenceId { get; set; }
        public bool? IsPublic { get; set; }
        public int? MediaId { get; set; }
    }
}