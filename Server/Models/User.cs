using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("displayed_name")]
        public string? DisplayedName { get; set; }
        [Column("tag")]
        public string? Tag { get; set; }
        [Column("bio")]
        public string? Bio { get; set; }
        [Column("birth_date")]
        public DateOnly? BirthDate { get; set; }
    }
}