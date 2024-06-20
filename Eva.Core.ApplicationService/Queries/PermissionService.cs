using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class PermissionService : BaseService<Permission, PermissionViewModel>, IPermissionService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public PermissionService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<CustomResultViewModel<PermissionViewModel>> CreatePermission(CreatePermissionViewModel model)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Permission permission = model;
                await context.Permissions.AddAsync(permission);
                await context.SaveChangesAsync();
                return new CustomResultViewModel<PermissionViewModel>()
                {
                    Entity = new PermissionViewModel() { Name = model.Name },
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Permission created successfully")
                };
            }
        }

        public async Task<IQueryable<string>> GetUserPermissions(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<string> permissions = await context
                    .Users
                    .Where(u => u.Id == userId)
                    .Join(
                        context.UserRoleMappings,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => userRole.RoleId
                        )
                    .Join(
                        context.Roles,
                        roleId => roleId,
                        role => role.Id,
                        (roleId, role) => role.Id
                    )
                    .Join(
                        context.RolePermissionMappings,
                        roleId => roleId,
                        permissionRole => permissionRole.RoleId,
                        (roleId, permissionRole) => permissionRole.PermissionId
                    ).
                    Join(
                        context.Permissions,
                        permissionId => permissionId,
                        permission => permission.Id,
                        (permissionId, permission) => permission.Name
                    )
                    .Distinct()
                    .ToListAsync();

                return permissions.AsQueryable();
            }
        }
    }
}
