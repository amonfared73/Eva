namespace Eva.Infra.EntityFramework.DbContexts
{
    /// <summary>
    /// This interface encapsulates EvaDbContext class
    /// </summary>
    public interface IEvaDbContext
    {
        public EvaDbContext EvaDbContext { get; set; }
    }
}
