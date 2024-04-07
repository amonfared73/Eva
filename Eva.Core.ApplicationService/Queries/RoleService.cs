using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class RoleService : BaseService<Role, RoleViewModel>, IRoleService
    {
        private readonly IEvaDbContextFactory _contextFactory;

        public RoleService(IEvaDbContextFactory contextFactory) : base(contextFactory)
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
