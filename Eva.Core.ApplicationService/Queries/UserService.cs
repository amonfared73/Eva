using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Hashers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public UserService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<User> GetByUsername(string username)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task Register(UserDto userDto)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                string passwordHash = PasswordHasher.Hash(userDto.Password);
                var registrationUser = new User()
                {
                    Username = userDto.Username,
                    PasswordHash = passwordHash,
                };
                await base.InsertAsync(registrationUser);
            }
        }
        public async Task<User> GetUserForLoginAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.Where(e => e.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<ActionResultViewModel<User>> AlterAdminStateAsync(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    throw new EvaNotFoundException(string.Format("{0} not found!", typeof(User).ToString()), typeof(User));

                var isAdmin = user.IsAdmin;
                user.IsAdmin = isAdmin ? false : true;
                await context.SaveChangesAsync();
                return new ActionResultViewModel<User>()
                {
                    Entity = user,
                    ResponseMessage = new Domain.Responses.ResponseMessage("User Updated successfully!")
                };
            }
        }

        public async Task<int> ExtractUserIdFromToken(HttpContext httpContext)
        {
            var userClaimId = httpContext.User.Claims.Where(x => x.Type == "id").FirstOrDefault();
            return await Task.FromResult(int.TryParse(userClaimId.Value, out int userId) ? userId : 0);
        }
        public async Task<int> ExtractUserIdFromRequestBody(string requestBody)
        {
            var loginViewModel = JsonConvert.DeserializeObject<LoginRequestViewModel>(requestBody);
            var user = await GetByUsername(loginViewModel.Username);
            return user.Id;
        }

        public async Task<int> GetUserIdFromContext(HttpContext httpContext, string requestBody)
        {
            var isLoginRequest = httpContext.Request.Path.Value == Authentication.LoginUrl;
            var userId = isLoginRequest ? await ExtractUserIdFromRequestBody(requestBody) : await ExtractUserIdFromToken(httpContext);
            return userId;
        }
    }
}
