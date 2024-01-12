using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Controllers
{
    [Authorize]
    public class DepartmentController : EvaControllerBase<Department>
    {
        private readonly IDepartmentService _baseService;
        public DepartmentController(IDepartmentService baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
