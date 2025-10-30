using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? DisplayedName { get; set; }
        public string? Tag { get; set; }
        public string? Bio { get; set; }
        public int Age { get; set; }
    }
}