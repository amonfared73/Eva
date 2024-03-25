using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IAesCryptographyService : IBaseService<AesCryptography, AesCryptographyViewModel>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
