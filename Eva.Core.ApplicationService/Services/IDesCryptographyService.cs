using Eva.Core.Domain.Models.Cryptography;

namespace Eva.Core.ApplicationService.Services
{
    public interface IDesCryptographyService : IBaseService<DesCryptography>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
