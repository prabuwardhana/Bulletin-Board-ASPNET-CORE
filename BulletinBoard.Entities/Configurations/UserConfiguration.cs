using BulletinBoard.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            builder.HasData
            (
                new User
                {
                    Id = "59d0b519-75a3-40c9-a6a8-2b42ad9e5095",
                    Name = "Administrator",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@email.com",
                    NormalizedEmail = "ADMIN@EMAIL.COM",
                    LockoutEnabled = true,
                    IsAdministrator = true,
                    PasswordHash = hasher.HashPassword(null, "temporarypass")
                }
            );
        }
    }
}