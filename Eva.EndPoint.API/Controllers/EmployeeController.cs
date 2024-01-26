using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Controllers
{
    public class EmployeeController : EvaControllerBase<Employee>
    {
        private readonly IEmployeeService _baseService;
        public EmployeeController(IEmployeeService baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
