using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("chats")]
    public class Chat
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
    }
}