using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class UserRoleMappingService : BaseService<UserRoleMapping>, IUserRoleMappingService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;

        public UserRoleMappingService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory) 
        {
            _contextFactory = contextFactory;
        }
    }
}
