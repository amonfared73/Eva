using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Controllers
{
    [Authorize]
    public class UserController : EvaControllerBase<User>
    {
        private readonly IUserService _service;
        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }
    }
}
