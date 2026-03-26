using Cozy_Chatter.Models;

namespace Cozy_Chatter.DTO
{
    public class MessageDTO
    {
        public string? Id { get; set; }
        public string? ChatId { get; set; }
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public string? TimeStamp { get; set; }
        public string? MessageReferenceId { get; set; }
        public string? IsPublic { get; set; }
        public string? MediaId { get; set; }
        public Message ConvertToMessage()
        {
            return new Message()
            {
                Id = Guid.Parse(Id),
                ChatId = int.Parse(ChatId),
                UserId = int.Parse(UserId),
                Content = Content,
                TimeStamp = DateTime.Parse(TimeStamp),
                MessageReferenceId = MessageReferenceId == null ? null : Guid.Parse(MessageReferenceId),
                IsPublic = IsPublic == null ? null : bool.Parse(IsPublic),
                MediaId = MediaId == null ? null : int.Parse(MediaId)
            };
        }
    }
}