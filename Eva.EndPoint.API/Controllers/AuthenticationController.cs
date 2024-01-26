using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.Tools.Hashers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class AuthenticationController : EvaControllerBase<Authentication>
    {
        private readonly IAuthenticationService _service;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;

        public AuthenticationController(IAuthenticationService service, IUserService userService, IRefreshTokenService refreshTokenRepository, RefreshTokenValidator refreshTokenValidator, Authenticator authenticator) : base(service)
        {
            _service = service;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
        }

        [HttpPost]
        [AllowAnonymous]
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
                PasswordHash = passwordHash
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
        public async Task<IActionResult> RefreshAsync(RefreshReuqest refreshReuqest)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshReuqest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new ResponseMessage("Invalid refresh token!"));
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshReuqest.RefreshToken);
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

        [HttpDelete]
        public async Task<IActionResult> LogoutAsync()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAllUserTokens(userId);
            return NoContent();
        }
    }
}
