using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class PermissionEndPointMappingConfiguration : IEntityTypeConfiguration<PermissionEndPointMapping>
    {
        public void Configure(EntityTypeBuilder<PermissionEndPointMapping> builder)
        {
            builder.HasIndex(i => new { i.PermissionId, i.EvaEndPointId }).IsUnique();
            builder.HasOne(i => i.Permission).WithMany(i => i.PermissionEndPointMappings).HasForeignKey(i => i.PermissionId);
            builder.HasOne(i => i.EvaEndPoint).WithMany(i => i.PermissionEndPointMappings).HasForeignKey(i => i.EvaEndPointId);
        }
    }
}
