namespace Eva.Infra.EntityFramework.DbContexts
{
    public interface IEvaDbContextFactory
    {
        EvaDbContext CreateDbContext();
    }
}
