﻿using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class RolePermissionService : BaseService<RolePermissionMapping, RolePermissionMappingViewModel>, IRolePermissionMappingService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public RolePermissionService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<CustomResultViewModel<RoleViewModel>> AppendPermissionToRole(AppendPermissionToRoleViewModel model)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var role = await context.Roles.FirstOrDefaultAsync(r => r.Id == model.RoleId);
                if (role == null)
                    throw new EvaNotFoundException("Role not found", typeof(Role));

                var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Id == model.PermissionId);
                if (permission == null)
                    throw new EvaNotFoundException("Role not found", typeof(Permission));

                RolePermissionMapping rolePermissionMapping = model;

                await context.RolePermissionMappings.AddAsync(rolePermissionMapping);
                await context.SaveChangesAsync();

                return new CustomResultViewModel<RoleViewModel>()
                {
                    Entity = new RoleViewModel() { Name = role.Name },
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage($"Permission: {permission.Name} successfully appended to Role: {role.Name}")
                };
            }
        }

        public async Task<IEnumerable<string>> ExtractUserPermissions(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var userRoles = await context.UserRoleMappings.Where(ur => ur.UserId == userId).Select(ur => new { ur.UserId, ur.RoleId }).ToListAsync();
                var permissions = await context.RolePermissionMappings.Include(p => p.Permission).Select(p => new { p.RoleId, p.PermissionId, p.Permission.Name }).ToListAsync();

                var userPermissions = (from userRole in userRoles
                                       join permission in permissions on userRole.RoleId equals permission.RoleId
                                       select permission.Name).Distinct();
                return userPermissions;
            }
        }
    }
}
