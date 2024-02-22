using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IDesCryptographyService : IBaseService<DesCryptography>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
