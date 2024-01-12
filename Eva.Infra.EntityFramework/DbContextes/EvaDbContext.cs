using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public class EvaDbContext : DbContext
    {
        public EvaDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvaDbContext).Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
