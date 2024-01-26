using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Controllers
{
    public class CompanyController : EvaControllerBase<Company>
    {
        private readonly ICompanyService _baseService;
        public CompanyController(ICompanyService baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
