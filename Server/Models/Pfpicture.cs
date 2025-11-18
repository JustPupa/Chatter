using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("pfpics")]
    public class Pfpicture
    {
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }
        [Column("picture_id", Order = 2)]
        public int PictureId { get; set; }
    }
}