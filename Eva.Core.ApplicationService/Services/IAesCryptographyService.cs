using Eva.Core.Domain.Models.Cryptography;

namespace Eva.Core.ApplicationService.Services
{
    public interface IAesCryptographyService : IBaseService<AesCryptography>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
