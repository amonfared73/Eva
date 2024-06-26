﻿using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.HasIndex(i => new { i.UserId, i.RoleId }).IsUnique();
            builder.HasOne(e => e.User).WithMany(u => u.UserRoleMappings).HasForeignKey(u => u.UserId);
            builder.HasOne(e => e.Role).WithMany(u => u.UserRoleMappings).HasForeignKey(u => u.RoleId);
        }
    }
}
