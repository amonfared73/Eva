using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.HasData(new User()
            {
                Id = 1,
                Username = "eva",
                PasswordHash = "$2a$11$GHlqmnSc71fIgIAi5d.d.eC5TyKpR2Z56.vU2vr/M36iLDHx8QhNy",
                Email = "eva@eva.com",
                IsAdmin = true,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                ModifiedBy = 1,
                ModifiedOn = DateTime.Now,
                DeletedOn = null,
                IsDeleted = false,
                Signature = null,
                StateCode = 1
            });
        }
    }
}
