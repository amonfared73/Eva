using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseInterfaces;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.Models.Inv;
using Eva.Infra.Tools.Extentions;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eva.Infra.EntityFramework.DbContexts
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
            var entities = Assemblies
                .GetEvaTypes(typeof(ISoftDelete))
                .Where(t => typeof(ModelBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && t.IsDefined(typeof(EvaEntityAttribute), true));
            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity).HasQueryFilter(GenerateQueryFilterLambda(entity));
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        ///     <para>
        ///         Overridden SaveChangesAsync method to manage trackable properties of Eva framework
        ///     </para>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information.
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_contextAccessor.IsLoginRequest())
            {
                // Grab all entity entries
                var entityEntries = ChangeTracker.Entries().Where(e => e.Entity is ModelBase && (e.State == EntityState.Added || e.State == EntityState.Modified));
                var userId = _contextAccessor.GetUserId();
                foreach (var entityEntry in entityEntries)
                {
                    // Check if entry state is added mode
                    var isAddedState = entityEntry.State == EntityState.Added;

                    // Detect trackable properties
                    entityEntry.Property("ModifiedOn").CurrentValue = DateTime.Now;
                    entityEntry.Property("ModifiedBy").CurrentValue = userId;
                    entityEntry.Property("CreatedOn").CurrentValue = isAddedState ? DateTime.Now : entityEntry.Property("CreatedOn").OriginalValue;
                    entityEntry.Property("CreatedBy").CurrentValue = isAddedState ? userId : entityEntry.Property("CreatedBy").OriginalValue;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// A utility method to provide a global query filter over all <see href="https://github.com/amonfared73/Eva">Eva</see> entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A LambdaExpression representing an equivalent form of .Where(e => !e.IsDeleted) </returns>
        private LambdaExpression GenerateQueryFilterLambda(Type entity)
        {
            var parameter = Expression.Parameter(entity, "e");
            var falseConstant = Expression.Constant(false);
            var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
            var equalExpression = Expression.Equal(propertyAccess, falseConstant);
            var lambda = Expression.Lambda(equalExpression, parameter);
            return lambda;
        }

        #region Eva framework entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<EvaLog> EvaLogs { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<EvaEndPoint> EvaEndPoints { get; set; }
        #endregion

        #region Cryptographic entities
        public DbSet<AesCryptography> AesCryptographies { get; set; }
        public DbSet<DesCryptography> DesCryptographies { get; set; }
        public DbSet<RsaCryptography> RsaCryptographies { get; set; }
        #endregion

        #region Business entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Complex> ComplexNumbers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        #endregion

        #region Inventory Managment
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryDocumentHeader> InventoryDocumentHeaders { get; set; }
        public DbSet<InventoryDocumentDetail> InventoryDocumentDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        #region General entities
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Good> Goods { get; set; }
        #endregion
    }
}
