using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Eva.Core.Domain.Responses;
using Eva.EndPoint.API.Authorization;
using Eva.Core.Domain.BaseModels;

namespace Eva.EndPoint.API.Controllers
{
    public class UserController : EvaControllerBase<User>
    {
        private readonly IUserService _service;
        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }

        [HttpPut]
        [HasRole(ActiveRoles.Admin)]
        public async Task<ActionResultViewModel<User>> AlterAdminStateAsync(int userId)
        {
            try
            {
                return await _service.AlterAdminStateAsync(userId);
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<User>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message),
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<User>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message),
                };
            }
        }

        // Assign all available roles to a particullar user
        // Only user with SystemDeveloper Role are allowed to use this endpoint
        [HttpPost]
        [HasRole(ActiveRoles.SystemDeveloper)]
        public async Task<ActionResultViewModel<User>> AssignAllRolesAsync(int userId)
        {
            return await _service.AssignAllRolesAsync(userId);
        }


        // Users must be registered through authentication controller
        // So insert action must be disbaled
        [NonAction]
        public override Task<ActionResultViewModel<User>> InsertAsync(User entity)
        {
            return base.InsertAsync(entity);
        }

        // Users are not allowed to be updated manually
        // They should be altered through their respective actions
        [NonAction]
        public override Task<ActionResultViewModel<User>> UpdateAsync(User entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
