using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public class EvaDbContextDesignTime : IDesignTimeDbContextFactory<EvaDbContext>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public EvaDbContextDesignTime(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public EvaDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=Eva.db";
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlite(connectionString);
            return new EvaDbContext(optionsBuilder.Options, _contextAccessor);
        }
    }
}
