using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using System.Security.Claims;

namespace Eva.Core.ApplicationService.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;
        private readonly IRolePermissionMappingService _rolePermissionMappingService;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator, IRolePermissionMappingService rolePermissionMappingService)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _rolePermissionMappingService = rolePermissionMappingService;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(CustomClaims.UserId, user.Id.ToString()),
                new Claim(CustomClaims.Username, user.Username),
                new Claim(CustomClaims.Email, user.Email ?? ""),
                new Claim(CustomClaims.IsAdmin, user.IsAdmin.ToString()),
                new Claim(CustomClaims.Signature, user.Signature ?? ""),
            };

            HashSet<string> permissions = await _rolePermissionMappingService.ExtractUserPermissions(user.Id);

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(CustomClaims.ActivePermissions, permission));
            }

            return _tokenGenerator.GenerateToken(
                _configuration.AccessTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccessTokenExpirationMinutes,
                claims);

        }
    }
}
