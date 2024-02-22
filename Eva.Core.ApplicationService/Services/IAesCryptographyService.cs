using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IAesCryptographyService : IBaseService<AesCryptography>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
