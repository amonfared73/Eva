using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public class EvaDbContextFactory : IEvaDbContextFactory
    {
        private readonly DbContextOptions _options;
        private readonly IHttpContextAccessor _contextAccessor;
        public EvaDbContextFactory(DbContextOptions options, IHttpContextAccessor contextAccessor)
        {
            _options = options;
            _contextAccessor = contextAccessor;
        }
        public EvaDbContext CreateDbContext()
        {
            return new EvaDbContext(_options, _contextAccessor);
        }
    }
}
