using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Responses;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class RefreshTokenService : BaseService<RefreshToken>, IRefreshTokenService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;

        public RefreshTokenService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateToken(RefreshToken refreshToken)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                context.RefreshTokens.Add(refreshToken);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTokenById(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                RefreshToken refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Id == id);
                if (refreshToken != null)
                {
                    context.RefreshTokens.Remove(refreshToken);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAllUserTokens(int userId)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<RefreshToken> refreshTokens = await context.RefreshTokens
                    .Where(t => t.UserId == userId)
                    .ToListAsync();
                context.RemoveRange(refreshTokens);
                await context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
            }
        }

        public async Task<ActionResultViewModel<RefreshToken>> ClearAllRefreshTokens()
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var refreshTokens = await context.RefreshTokens.ToListAsync();
                context.RefreshTokens.RemoveRange(refreshTokens);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<RefreshToken>()
                {
                    Entity = null,
                    HasError = false,
                    ResponseMessage = new ResponseMessage("All refresh tokens cleared")
                };
            }
        }
    }
}
