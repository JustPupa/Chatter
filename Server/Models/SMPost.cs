using Cozy_Chatter.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("SM_Posts")]
    public class SMPost
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? Publisher { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }
        public int? Post_Ref { get; set; }
        public SMPost? ReferencePost { get; set; }
        public DateTime? Pub_time { get; set; }
    }
}