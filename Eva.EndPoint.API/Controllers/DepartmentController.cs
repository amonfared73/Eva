using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class DepartmentController : EvaControllerBase<Department, DepartmentViewModel>
    {
        private readonly IDepartmentService _baseService;
        public DepartmentController(IDepartmentService baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
