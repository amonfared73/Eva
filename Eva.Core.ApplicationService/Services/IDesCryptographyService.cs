using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IDesCryptographyService : IBaseService<DesCryptography, DesCryptographyViewModel>
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string cipherText);
    }
}
