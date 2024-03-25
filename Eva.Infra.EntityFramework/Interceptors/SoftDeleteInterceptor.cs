using Eva.Core.Domain.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eva.Infra.EntityFramework.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entries = eventData
                .Context
                .ChangeTracker
                .Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDelete> entry in entries)
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete deletedEntity })
                    continue;

                entry.State = EntityState.Modified;
                deletedEntity.IsDeleted = true;
                deletedEntity.DeletedOn = DateTime.Now;

            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
