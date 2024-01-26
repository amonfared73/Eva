using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // Users must be registered through authentication controller
        // So insert action must be disbaled
        [NonAction]
        public override Task<ActionResultViewModel<User>> InsertAsync(User entity)
        {
            return base.InsertAsync(entity);
        }
    }
}
