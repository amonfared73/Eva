﻿using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;

        public RoleService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ActionResultViewModel<Role>> CreateRole(string name)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var role = new Role()
                {
                    Name = name
                };
                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Role>()
                {
                    Entity = role,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Role created successfully!")
                };
            }
        }
    }
}
