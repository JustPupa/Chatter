using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("Emojis")]
    public class Emoji
    {
        [Key]
        public int Id { get; set; }
        public string? HtmlCode { get; set; }
        public bool? IsDefault { get; set; }
    }
}