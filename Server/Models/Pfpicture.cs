using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozy_Chatter.Models
{
    [Table("PfPics")]
    public class Pfpicture
    {
        [Column("UserId", Order = 1)]
        public int UserId { get; set; }
        [Column("PictureId", Order = 2)]
        public int PictureId { get; set; }
    }
}
