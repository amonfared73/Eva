using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eva.Infra.EntityFramework.Configurations
{
    public class EvaLogConfiguration : IEntityTypeConfiguration<EvaLog>
    {
        public void Configure(EntityTypeBuilder<EvaLog> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
