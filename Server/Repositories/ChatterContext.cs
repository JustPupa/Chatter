using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ChatterContext(DbContextOptions<ChatterContext> options) : DbContext(options)
    {
        public DbSet<AllowedEmoji> AllowedEmojis { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Emoji> Emojis { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessagePin> MessagePins { get; set; }
        public DbSet<Pfpicture> Pfpictures { get; set; }
        public DbSet<SMPost> SMPosts { get; set; }
        public DbSet<SMPostLike> SMPostLikes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReaction> UserReactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AllowedEmoji>().HasKey(emj => new { emj.PostId, emj.EmojiId });
            builder.Entity<ChatUser>().HasKey(ch => new { ch.ChatId, ch.UserId });
            builder.Entity<MessagePin>().HasKey(mes => new { mes.MessageId, mes.UserId });
            builder.Entity<Pfpicture>().HasKey(pfp => new { pfp.UserId, pfp.PictureId });
            builder.Entity<SMPostLike>().HasKey(pl => new { pl.UserId, pl.PostId });
            builder.Entity<Subscription>().HasKey(sub => new { sub.UserId, sub.FollowerId });
            builder.Entity<UserReaction>().HasKey(rct => new { rct.UserId, rct.PostId });

            builder.Entity<SMPost>(entity =>
            {
                entity.HasOne(p => p.Publisher)
                      .WithMany()
                      .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.ReferencePost)
                      .WithMany()
                      .HasForeignKey(p => p.Post_Ref);
            });

            builder.Entity<ChatUser>(entity =>
            {
                entity.HasKey(cu => new { cu.ChatId, cu.UserId });

                entity.HasOne(cu => cu.Chat)
                      .WithMany(c => c.ChatUsers)
                      .HasForeignKey(cu => cu.ChatId);

                entity.HasOne(cu => cu.User)
                      .WithMany(u => u.ChatUsers)
                      .HasForeignKey(cu => cu.UserId);
            });

            builder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats)
                .UsingEntity<ChatUser>();
        }
    }
}