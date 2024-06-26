﻿using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class UserRoleMappingService : BaseService<UserRoleMapping, UserRoleMappingViewModel>, IUserRoleMappingService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public UserRoleMappingService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<ActionResultViewModel<UserRoleMapping>> AddRoleToUserAsync(UserRoleMappingDto request)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                // Find respective userId
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
                if (user == null)
                    throw new EvaNotFoundException(string.Format("{0} not found!", typeof(User).ToString()), typeof(User));

                // Find respective roleId
                var role = await context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);
                if (role == null)
                    throw new EvaNotFoundException(string.Format("{0} not found!", typeof(Role).ToString()), typeof(Role));

                // Append role to user
                await context.UserRoleMappings.AddAsync(new UserRoleMapping()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
                await context.SaveChangesAsync();
                return new ActionResultViewModel<UserRoleMapping>()
                {
                    ResponseMessage = new ResponseMessage("Role appended to user successfully")
                };
            }
        }

        public async Task<HashSet<string>> GetRolesForUserAsync(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var roles = await context.UserRoleMappings.Where(e => e.UserId == userId).Select(r => r.Role).ToArrayAsync();
                return roles.Select(r => r.Name).ToHashSet();
            }
        }

        public async Task<IEnumerable<UserRolesViewModel>> UserRolesReport(int? userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var users = await context.Users.Where(u => u.Id == userId || userId == null).ToListAsync();
                var roles = await context.Roles.ToListAsync();
                var userRoles = await context.UserRoleMappings.ToListAsync();

                var query = from userRole in userRoles
                            join user in users on userRole.UserId equals user.Id
                            join role in roles on userRole.RoleId equals role.Id
                            select new UserRolesViewModel()
                            {
                                Username = user.Username,
                                RoleName = role.Name
                            };
                return query;
            }
        }
    }
}
