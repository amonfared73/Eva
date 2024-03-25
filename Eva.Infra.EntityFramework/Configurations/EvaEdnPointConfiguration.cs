using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class EvaEdnPointConfiguration : IEntityTypeConfiguration<EvaEndPoint>
    {
        public void Configure(EntityTypeBuilder<EvaEndPoint> builder)
        {
            builder.HasIndex(e => e.Url).IsUnique();
        }
    }
}
