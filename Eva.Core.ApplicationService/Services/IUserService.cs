﻿using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<ActionResultViewModel<User>> AlterAdminStateAsync(int userId);
        Task<User> GetByUsername(string username);
        Task<User> GetUserForLoginAsync(int id);
        Task Register(UserDto userDto);
        Task<int> ExtractUserIdFromToken(HttpContext httpContext);
        Task<int> ExtractUserIdFromRequestBody(string requestBody);
        Task<int> GetUserIdFromContext(HttpContext httpContext, string requestBody);
    }
}
