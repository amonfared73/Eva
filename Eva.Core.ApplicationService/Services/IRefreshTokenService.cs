using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRefreshTokenService : IBaseService<RefreshToken>
    {
        Task<RefreshToken> GetByToken(string token);
        Task CreateToken(RefreshToken refreshToken);
        Task DeleteTokenById(int id);
        Task DeleteAllUserTokens(int userId);
        Task<ActionResultViewModel<RefreshToken>> ClearAllRefreshTokens();
    }
}
