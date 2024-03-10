using Microsoft.EntityFrameworkCore;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public interface IEvaDbContextFactory : IDbContextFactory<EvaDbContext>
    {
    }
}
