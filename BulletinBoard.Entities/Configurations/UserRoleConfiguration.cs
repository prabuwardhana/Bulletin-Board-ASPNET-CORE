using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Entities.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "f6d54222-3065-48b8-86e3-8c0c7ad6e7cc",
                    UserId = "59d0b519-75a3-40c9-a6a8-2b42ad9e5095"
                }
            );
        }
    }
}