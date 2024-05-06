using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class RolePermissionMappingConfiguration : IEntityTypeConfiguration<RolePermissionMapping>
    {
        public void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
        {
            builder.HasIndex(i => new { i.RoleId, i.PermissionId }).IsUnique();
            builder.HasOne(i => i.Role).WithMany(r => r.RolePermissionMappings).HasForeignKey(u => u.RoleId);
            builder.HasOne(i => i.Permission).WithMany(r => r.RolePermissionMappings).HasForeignKey(u => u.PermissionId);
        }
    }
}
