using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("chats")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
