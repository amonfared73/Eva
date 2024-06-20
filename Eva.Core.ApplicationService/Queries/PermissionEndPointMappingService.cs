using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.EntityFramework.Migrations;
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

        public async Task<ActionResultViewModel<PermissionEndPointMapping>> AppendEndPointToPermission(PermissionEndpointMappingDto model)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Id == model.PermissionId);
                if (permission == null)
                    throw new EvaNotFoundException("Permission not found", typeof(Permission));

                var evaEndPoint = await context.EvaEndPoints.FirstOrDefaultAsync(e => e.Id == model.EvaEndPointId);
                if (evaEndPoint == null)
                    throw new EvaNotFoundException("EndPoint not found", typeof(EvaEndPoint));

                PermissionEndPointMapping permissionEndpointMapping = new PermissionEndPointMapping()
                {
                    PermissionId = model.PermissionId,
                    EvaEndPointId = model.EvaEndPointId
                };

                await context.PermissionEndPointMappings.AddAsync(permissionEndpointMapping);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<PermissionEndPointMapping>()
                {
                    Entity = permissionEndpointMapping,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage($"Endpoint {evaEndPoint.Url} successfully appended to permission {permission.Name}")
                };
            }
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
                    .Distinct()
                    .ToListAsync();

                return accessibleEndpoints.ToHashSet(); ;
            }
        }
    }
}
