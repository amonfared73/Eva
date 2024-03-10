namespace Eva.Infra.EntityFramework.DbContextes
{
    public interface IEvaDbContextFactory
    {
        EvaDbContext CreateDbContext();
    }
}
