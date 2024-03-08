using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Infra.Tools.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eva.Infra.EntityFramework.DbContextes
{
    public class EvaDbContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public EvaDbContext(DbContextOptions options) : base(options)
        {

        }
        public EvaDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_contextAccessor.IsLoginRequeust())
            {
                // Grab all entity entries
                var entityEntries = ChangeTracker.Entries().Where(e => e.Entity is DomainObject && (e.State == EntityState.Added || e.State == EntityState.Modified));
                var userId = _contextAccessor.GetUserId();
                foreach (var entityEntry in entityEntries)
                {
                    var createdOn = entityEntry.Property("CreatedOn").CurrentValue;
                    var createdBy = entityEntry.Property("CreatedBy").CurrentValue;

                    entityEntry.Property("ModifiedOn").CurrentValue = DateTime.Now;
                    entityEntry.Property("ModifiedBy").CurrentValue = userId;

                    entityEntry.Property("CreatedOn").CurrentValue = createdOn is null ? DateTime.Now : createdBy;
                    entityEntry.Property("CreatedBy").CurrentValue = createdBy is null ? userId : createdBy;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
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
        public DbSet<Instrument> Instruments { get; set; }
    }
}
