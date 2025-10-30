using Cozy_Chatter.DTOs;
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
        public string? Content { get; set; }
        public DateTime? Date { get; set; }
        public int? Post_Ref { get; set; }
        public DateTime? Pub_time { get; set; }
        public static SMPostFull ToDetailed(SMPost post)
        {
            var postDetails = new SMPostFull
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                Date = post.Date,
                Post_Ref = post.Post_Ref,
                Pub_time = post.Pub_time,
                Publisher = UserRepository.GetUserById(post.UserId).Result
            };
            if (post.Post_Ref is not null)
            {
                postDetails.ReferencePostFull = ToDetailed(SMPostRepository.GetPostById(post.Post_Ref.Value).Result);
            }
            return postDetails;
        }
    }
}