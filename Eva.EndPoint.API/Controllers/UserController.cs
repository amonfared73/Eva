using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Eva.Core.Domain.Responses;
using Eva.EndPoint.API.Authorization;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class UserController : EvaControllerBase<User, UserViewModel>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [HasAccess(ActivePermissions.Encrypt)]
        public async Task<ActionResultViewModel<User>> AlterAdminStateAsync(int userId)
        {
            try
            {
                return await _userService.AlterAdminStateAsync(userId);
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

        // Assign all available roles to a particular user
        // Only user with SystemDeveloper Role are allowed to use this endpoint
        [HttpPost]
        [HasAccess(ActivePermissions.Encrypt)]
        public async Task<ActionResultViewModel<User>> AssignAllMissingRolesAsync(int userId)
        {
            try
            {
                return await _userService.AssignAllMissingRolesAsync(userId);
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<User>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage($"{ex.Message}"),
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<User>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage($"{ex.Message}"),
                };
            }
        }


        // Users must be registered through authentication controller
        // So insert action must be disabled
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
        [HttpPost]
        public async Task<CustomResultViewModel<string>> CreateUserSignature(string signatureBase)
        {
            try
            {
                var userId = User.FindFirst(CustomClaims.UserId)?.Value;
                if (userId is null)
                    throw new EvaNotFoundException($"Unable to find current user", typeof(User));
                int.TryParse(userId, out int parsedUserId);
                return await _userService.CreateUserSignature(parsedUserId, signatureBase);
            }
            catch (EvaNotFoundException ex)
            {
                return new CustomResultViewModel<string>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new CustomResultViewModel<string>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost]
        public async Task<CustomResultViewModel<string>> ClearUserSignature(string signatureBase)
        {
            try
            {
                var userId = User.FindFirst(CustomClaims.UserId)?.Value;
                if (userId is null)
                    throw new EvaNotFoundException($"Unable to find current user", typeof(User));
                int.TryParse(userId, out int parsedUserId);
                return await _userService.ClearUserSignature(parsedUserId, signatureBase);
            }
            catch (EvaNotFoundException ex)
            {
                return new CustomResultViewModel<string>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new CustomResultViewModel<string>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost]
        public async Task<UserValidatorResponseViewModel> ValidateUserAsync(int userId)
        {
            return await _userService.ValidateUserAsync(userId);
        }

        [HttpPost]
        public async Task<ActionResultViewModel<User>> ProcessUserPermissionAsync(string username)
        {
            try
            {
                return await _userService.ProcessUserPermissionAsync(username);
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<User>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage($"{ex.Message}")
                };
            }
        }
    }
}
