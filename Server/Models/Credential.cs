using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("credentials")]
    public class Credential
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("login")]
        public string? Login { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("phonenumber")]
        public string? PhoneNumber { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("salt")]
        public byte[]? Salt { get; set; }
        public User User { get; set; } = new();
    }
}