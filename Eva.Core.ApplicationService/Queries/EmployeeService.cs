using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public EmployeeService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
