using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.Tools.Hashers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class AuthenticationController : EvaControllerBase<Authentication, AuthenticationViewModel>
    {
        private readonly IAuthenticationService _service;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;
        private readonly IMemoryCache _memoryCache;
        private readonly EvaCachingKeys _keys;
        private readonly IUserContext _userContext;

        public AuthenticationController(IAuthenticationService service, IUserService userService, IRefreshTokenService refreshTokenRepository, RefreshTokenValidator refreshTokenValidator, Authenticator authenticator, IMemoryCache memoryCache, EvaCachingKeys keys, IUserContext userContext) : base(service)
        {
            _service = service;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
            _memoryCache = memoryCache;
            _keys = keys;
            _userContext = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserDto userDto)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            // Check confirmation password
            if (userDto.Password != userDto.ConfirmPassword)
                return BadRequest(new ResponseMessage("Password does not match confirm password!"));

            // Check existing username
            var existingUserByUsername = await _userService.GetByUsername(userDto.Username);
            if (existingUserByUsername != null)
                return Conflict(new ResponseMessage("Username already exist!"));

            // Creating the user
            string passwordHash = PasswordHasher.Hash(userDto.Password);
            var registrationUser = new User()
            {
                Username = userDto.Username,
                PasswordHash = passwordHash,
                Email = userDto.Email,
            };

            await _userService.InsertAsync(registrationUser);
            return Ok(new ResponseMessage(string.Format("Username: {0} created successfully", registrationUser.ToString())));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginRequestViewModel login)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            User user = await _userService.GetByUsername(login.Username);

            if (user == null)
                return Unauthorized(new ResponseMessage("User not found!"));

            bool isCorrectPassword = PasswordHasher.Verify(login.Password, user.PasswordHash);
            if (!isCorrectPassword)
                return Unauthorized(new ResponseMessage("Incorrect password!"));

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAsync(RefreshReuqest refreshRequest)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new ResponseMessage("Invalid refresh token!"));
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                return NotFound(new ResponseMessage("Invalid refresh token!"));
            }

            await _refreshTokenRepository.DeleteTokenById(refreshTokenDTO.Id);

            User user = await _userService.GetUserForLoginAsync(refreshTokenDTO.UserId);
            if (user == null)
            {
                return NotFound(new ResponseMessage("User not found!"));
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync(PasswordChangeViewModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.CurrentPassword))
                    throw new EvaRequiredPropertyException("Please enter your current password");

                if (string.IsNullOrEmpty(request.NewPassword))
                    throw new EvaRequiredPropertyException("Please enter your new password");

                var rawUserId = HttpContext.User.FindFirstValue(CustomClaims.UserId);
                if (!int.TryParse(rawUserId, out int userId))
                    return Unauthorized();

                var result = await _userService.GetByIdAsync(userId);
                var user = result.Entity as User;
                if (user is null)
                    throw new Exception("User not found");

                bool isCorrectPassword = PasswordHasher.Verify(request.CurrentPassword, user.PasswordHash);
                if (!isCorrectPassword)
                    throw new Exception("Your current password is incorrect");

                await _userService.ChangePasswordAsync(userId, request);
                return Ok(result);
            }
            catch (EvaRequiredPropertyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> LogoutAsync()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAllUserTokens(userId);
            _memoryCache.Remove($"{_keys.AccessiblePermissions} {_userContext.UserId}");
            return NoContent();
        }
    }
}
