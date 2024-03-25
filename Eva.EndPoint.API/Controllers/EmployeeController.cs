using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class EmployeeController : EvaControllerBase<Employee, EmployeeViewModel>
    {
        private readonly IEmployeeService _baseService;
        public EmployeeController(IEmployeeService baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
