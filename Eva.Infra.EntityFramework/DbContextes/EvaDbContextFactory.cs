using Microsoft.EntityFrameworkCore;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public class EvaDbContextFactory : IDbContextFactory<EvaDbContext>
    {
        private readonly DbContextOptions _options;
        public EvaDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }
        public EvaDbContext CreateDbContext()
        {
            return new EvaDbContext(_options);
        }
    }
}
