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
        private readonly IUserRoleMappingService _userRoleMappingService;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator, IUserRoleMappingService userRoleMappingService)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _userRoleMappingService = userRoleMappingService;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(CustomClaims.IsAdmin, user.IsAdmin.ToString()),
                new Claim(CustomClaims.Signature, user.Signature),
            };

            HashSet<string> roles = await _userRoleMappingService.GetRolesForUserAsync(user.Id);

            foreach (var role in roles)
            {
                claims.Add(new Claim(CustomClaims.ActiveRoles, role));
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
