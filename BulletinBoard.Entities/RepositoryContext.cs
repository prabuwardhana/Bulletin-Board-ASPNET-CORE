using BulletinBoard.Entities.Configurations;
using BulletinBoard.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Forum> Forum { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.Entity<Forum>(entity =>
            {
                // Configure relationship between Forum entity and User Entity
                entity
                    // navigation property of Forum entity.
                    .HasOne(forum => forum.Owner)
                    // inverse navigation property.
                    // This references the collection navigation property on User entity (User.Forums).
                    // Specifies that one User can have multiple Forums
                    .WithMany(user => user.Forums)
                    // foreign key for this particular relationship
                    .HasForeignKey(forum => forum.OwnerId)
                    // optional relationships
                    // Dependent entity (User) can still exist without principal entity (Forum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Forum_Owner");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(msg => msg.FromUser)
                    .WithMany(user => user.MessagesFromUser)
                    .HasForeignKey(msg => msg.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_FromUser");

                entity.HasOne(msg => msg.ToUser)
                    .WithMany(user => user.MessagesToUser)
                    .HasForeignKey(msg => msg.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_ToUser");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasOne(topic => topic.Forum)
                    .WithMany(forum => forum.Topic)
                    .HasForeignKey(topic => topic.ForumId)
                    .HasConstraintName("FK_Topic_Forum");

                entity.HasOne(topic => topic.ModifiedByUser)
                    .WithMany(user => user.TopicsModifiedByUser)
                    .HasForeignKey(topic => topic.ModifiedByUserId)
                    .HasConstraintName("FK_Topic_ModifiedByUser");

                entity.HasOne(topic => topic.Owner)
                    .WithMany(user => user.TopicsOwner)
                    .HasForeignKey(topic => topic.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic_Owner");

                entity.HasOne(topic => topic.ReplyToTopic)
                    .WithMany(topic => topic.InverseReplyToTopic)
                    .HasForeignKey(topic => topic.ReplyToTopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Topic_ReplyToTopic");

                entity.HasOne(topic => topic.RootTopic)
                    .WithMany(topic => topic.InverseRootTopic)
                    .HasForeignKey(topic => topic.RootTopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Topic_RootTopic");
            });

            modelBuilder.Entity<UserNotification>(entity => 
            {
                entity.HasKey(k => new { k.NotificationId, k.UserId });
            });
        }
    }
}