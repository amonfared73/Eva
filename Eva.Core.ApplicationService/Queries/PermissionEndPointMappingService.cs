using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class PermissionEndPointMappingService : BaseService<PermissionEndPointMapping, PermissionEndPointMappingViewModel>, IPermissionEndPointMappingService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public PermissionEndPointMappingService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<HashSet<string>> GetAccessibleEndPoints(IEnumerable<string> permissions)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var accessibleEndpoints = await context
                    .PermissionEndPointMappings
                    .Include(e => e.Permission)
                    .Include(e => e.EvaEndPoint)
                    .Where(e => permissions.Contains(e.Permission.Name))
                    .Select(e => e.EvaEndPoint.Url)
                    .ToListAsync();

                return accessibleEndpoints.ToHashSet(); ;
            }
        }
    }
}
