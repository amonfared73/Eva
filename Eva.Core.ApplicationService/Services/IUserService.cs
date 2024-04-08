using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Services
{
    public interface IUserService : IBaseService<User, UserViewModel>
    {
        Task<ActionResultViewModel<User>> AlterAdminStateAsync(int userId);
        Task<User> GetByUsername(string username);
        Task<User> GetUserForLoginAsync(int id);
        Task Register(UserDto userDto);
        Task<int> ExtractUserIdFromToken(HttpContext httpContext);
        Task<int> ExtractUserIdFromRequestBody(string requestBody);
        Task<int> GetUserIdFromContext(HttpContext httpContext, string requestBody);
        Task<ActionResultViewModel<User>> AssignAllMissingRolesAsync(int userId);
        Task<CustomResultViewModel<string>> CreateUserSignature(int userId, string signatureBase);
        Task<CustomResultViewModel<string>> ClearUserSignature(int userId, string signatureBase);
        Task<UserValidatorResponseViewModel> ValidateUserAsync(int userId);
        Task ChangePasswordAsync(int userId, PasswordChangeViewModel result);
    }
}
