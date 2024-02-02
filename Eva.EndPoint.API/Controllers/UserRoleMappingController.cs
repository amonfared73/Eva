using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class UserRoleMappingController : EvaControllerBase<UserRoleMapping>
    {
        private readonly IUserRoleMappingService _service;

        public UserRoleMappingController(IUserRoleMappingService service) : base(service)
        {
            _service = service;
        }
        [NonAction]
        public override Task<ActionResultViewModel<UserRoleMapping>> InsertAsync(UserRoleMapping entity)
        {
            return base.InsertAsync(entity);
        }
        [NonAction]
        public override Task<ActionResultViewModel<UserRoleMapping>> UpdateAsync(UserRoleMapping entity)
        {
            return base.UpdateAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<UserRoleMapping>> AddRoleToUserAsync(UserRoleMappingDto request)
        {
            try
            {
                return await _service.AddRoleToUserAsync(request);
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<UserRoleMapping>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message),
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<UserRoleMapping>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message),
                };
            }
        }
    }
}
