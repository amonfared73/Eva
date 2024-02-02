using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.HasKey(i => new { i.UserId, i.RoleId });
            builder.HasOne(e => e.User).WithMany(u => u.UserRoleMapping).HasForeignKey(u => u.UserId);
            builder.HasOne(e => e.Role).WithMany(u => u.UserRoleMapping).HasForeignKey(u => u.RoleId);
        }
    }
}
