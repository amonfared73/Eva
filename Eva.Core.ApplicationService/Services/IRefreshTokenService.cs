using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRefreshTokenService : IBaseService<RefreshToken, RefreshTokenViewModel>
    {
        Task<RefreshToken> GetByToken(string token);
        Task CreateToken(RefreshToken refreshToken);
        Task DeleteTokenById(int id);
        Task DeleteAllUserTokens(int userId);
        Task<ActionResultViewModel<RefreshToken>> ClearAllRefreshTokens();
    }
}
