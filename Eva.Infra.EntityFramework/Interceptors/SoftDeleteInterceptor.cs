using Eva.Core.Domain.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eva.Infra.EntityFramework.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete deletedEntity })
                    continue;

                entry.State = EntityState.Modified;
                deletedEntity.IsDeleted = true;
                deletedEntity.DeletedOn = DateTime.Now;

            }

            return result;
        }
    }
}
