using Eva.Core.Domain.Models.Cryptography;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRsaCryptographyService : IBaseService<RsaCryptography>
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}
