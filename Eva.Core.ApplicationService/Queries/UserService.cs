using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Validators;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.Tools.Extensions;
using Eva.Infra.Tools.Hashers;
using Eva.Infra.Tools.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class UserService : BaseService<User, UserViewModel>, IUserService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        private readonly IUserRoleMappingService _userRoleMappingService;
        private readonly IRsaCryptographyService _rsaCryptographyService;
        private readonly UserValidator _userValidator;
        public UserService(IEvaDbContextFactory contextFactory, IUserRoleMappingService userRoleMappingService, IRsaCryptographyService rsaCryptographyService, UserValidator userValidator) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _userRoleMappingService = userRoleMappingService;
            _rsaCryptographyService = rsaCryptographyService;
            _userValidator = userValidator;
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
                    ResponseMessage = new ResponseMessage("User Updated successfully!")
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
            var userId = httpContext.IsLoginRequest() ? await ExtractUserIdFromRequestBody(requestBody) : await ExtractUserIdFromToken(httpContext);
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

        public async Task<CustomResultViewModel<string>> CreateUserSignature(int userId, string signatureBase)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                // Grab user
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    throw new EvaNotFoundException("User not found", typeof(User));

                // Check if user already has a signature
                if (!string.IsNullOrEmpty(user.Signature))
                    throw new Exception("You already have a predefined signature, if you need to change it, clear it first");

                // Create signature
                var signature = new UserSignatureViewModel()
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    CreatedOn = user.CreatedOn,
                    SignatureBase = signatureBase
                }.ToJson();

                // Encrypt signature
                var encryptedSignature = _rsaCryptographyService.Encrypt(signature);

                // Assign signature
                user.Signature = encryptedSignature;
                await context.SaveChangesAsync();

                return new CustomResultViewModel<string>()
                {
                    Entity = signature,
                    HasError = false,
                    ResponseMessage = new ResponseMessage("Signature created successfully!")
                };

            }
        }

        public async Task<CustomResultViewModel<string>> ClearUserSignature(int userId, string signatureBase)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                // Check if signatureBase is empty
                if (string.IsNullOrEmpty(signatureBase))
                    throw new Exception("Signature base can not be empty");

                // Grab user
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    throw new EvaNotFoundException("User not found", typeof(User));

                // Check if user's signature is empty
                if (string.IsNullOrEmpty(user.Signature))
                    throw new Exception("User's signature is already empty");

                // Validate signature
                var encryptedSignature = user.Signature;
                var incomingSignature = new UserSignatureViewModel()
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    CreatedOn = user.CreatedOn,
                    SignatureBase = signatureBase
                }.ToJson();
                if (!_rsaCryptographyService.Verify(incomingSignature, encryptedSignature))
                    throw new Exception("Your signature credentials is not correct");

                // Clear signature
                user.Signature = string.Empty;
                await context.SaveChangesAsync();

                return new CustomResultViewModel<string>()
                {
                    Entity = null,
                    HasError = false,
                    ResponseMessage = new ResponseMessage($"{user.Username} signature cleared")
                };
            }
        }

        public async Task<UserValidatorResponseViewModel> ValidateUserAsync(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user is null)
                    throw new EvaNotFoundException("User not found", typeof(User));

                var isValidUserResponse = _userValidator.Validate(user);

                return new UserValidatorResponseViewModel()
                {
                    IsValid = isValidUserResponse.IsValid,
                    ResponseMessage = isValidUserResponse.ResponseMessage,
                };
            }
        }

        public async Task ChangePasswordAsync(int userId, PasswordChangeViewModel request)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if(user is null)
                    throw new EvaNotFoundException("User not found", typeof(User));

                user.PasswordHash = PasswordHasher.Hash(request.NewPassword);
                await context.SaveChangesAsync();
            }
        }
    }
}
