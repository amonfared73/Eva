using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Hashers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        private readonly IUserRoleMappingService _userRoleMappingService;
        private readonly IAesCryptographyService _aesCryptographyService;
        public UserService(IDbContextFactory<EvaDbContext> contextFactory, IUserRoleMappingService userRoleMappingService, IAesCryptographyService aesCryptographyService) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _userRoleMappingService = userRoleMappingService;
            _aesCryptographyService = aesCryptographyService;
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
        public async Task<int?> ExtractUserIdFromRequestBody(string requestBody)
        {
            var loginViewModel = JsonConvert.DeserializeObject<LoginRequestViewModel>(requestBody);
            var user = await GetByUsername(loginViewModel.Username);
            return user == null ? null : user.Id;
        }

        public async Task<int?> GetUserIdFromContext(HttpContext httpContext, string requestBody)
        {
            var isLoginRequest = httpContext.Request.Path.Value == Authentication.LoginUrl;
            var userId = isLoginRequest ? await ExtractUserIdFromRequestBody(requestBody) : await ExtractUserIdFromToken(httpContext);
            return userId;
        }

        public async Task<ActionResultViewModel<User>> AssignAllMissingRolesAsync(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                // Find the user
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    throw new EvaNotFoundException(string.Format("{0} not found!", typeof(User).ToString()), typeof(User));

                // Find the roles that the user currently has
                var userRoles = await context.UserRoleMappings.Where(u => u.UserId == userId).Select(r => new { r.Id, r.RoleId, r.UserId }).ToListAsync();

                // Grab all roles
                var roles = await context.Roles.Select(r => new { r.Id, r.Name }).ToListAsync();

                // Find the missing roles need to be assigned to the user
                var missingRoles = from role in roles
                                   join userRole in userRoles on role.Id equals userRole.RoleId into currentUserRoles
                                   from currentUserRole in currentUserRoles.DefaultIfEmpty()
                                   where currentUserRole == null
                                   select role;

                // Check if there is any role to add
                if (!missingRoles.Any())
                    throw new EvaNotFoundException($"{user.Username} has all the existing roles, no roles added", typeof(User));

                // Add roles to the mapping table
                foreach (var role in missingRoles)
                {
                    await _userRoleMappingService.AddRoleToUserAsync(new UserRoleMappingDto
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    });
                }

                // Concat all role names
                var addedRoles = String.Join(",", missingRoles);

                // Return
                return new ActionResultViewModel<User>()
                {
                    Entity = user,
                    HasError = false,
                    ResponseMessage = new ResponseMessage($"Roles added successfully, roles: {addedRoles}")
                };
            }
        }

        public async Task<CustomActionResultViewModel<string>> CreateUserSignature(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    throw new EvaNotFoundException("User no found", typeof(User));

                var signature = $"{user.Id.ToString()} | {user.Username} | {DateTime.Now.ToString()}";
                var encryptedSignature = await _aesCryptographyService.Encrypt(signature);

                user.Signature = encryptedSignature;
                await context.SaveChangesAsync();

                return new CustomActionResultViewModel<string>()
                {
                    Entity = signature,
                    HasError = false,
                    ResponseMessage = new ResponseMessage("Signature created successfully!")
                };

            }
        }
    }
}
