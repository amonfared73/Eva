using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRsaCryptographyService : IBaseService<RsaCryptography, RsaCryptographyViewModel>
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
        bool Verify(string plainText, string encryptedText);
    }
}
