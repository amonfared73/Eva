using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

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
    }
}
