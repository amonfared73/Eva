using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Models.Cryptography;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Overriden SaveChangesAsync method to implement trackable properties of Eva entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Grab all entity entries
            var entityEntries = ChangeTracker.Entries().Where(e => e.Entity is DomainObject && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entityEntries)
            {
                // Add current datetime for added state
                if (entityEntry.State == EntityState.Added)
                    ((DomainObject)entityEntry.Entity).CreatedOn = DateTime.Now;
                // Add current datetime for modified state
                ((DomainObject)entityEntry.Entity).ModifiedOn = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<EvaLog> EvaLogs { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AesCryptography> AesCryptographies { get; set; }
        public DbSet<DesCryptography> DesCryptographies { get; set; }
        public DbSet<RsaCryptography> RsaCryptographies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Complex> ComplexNumbers { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
