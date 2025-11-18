using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("emojis")]
    public class Emoji
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("htmlcode")]
        public string? HtmlCode { get; set; }
        [Column("is_default")]
        public bool? IsDefault { get; set; }
    }
}