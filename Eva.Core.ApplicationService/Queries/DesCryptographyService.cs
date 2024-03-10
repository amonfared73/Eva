using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class DesCryptographyService : BaseService<DesCryptography>, IDesCryptographyService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        private readonly DesEncryptor _encryptor;

        public DesCryptographyService(IEvaDbContextFactory contextFactory, DesEncryptor encryptor) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _encryptor = encryptor;
        }
        public async Task<string> Encrypt(string text)
        {
            return await _encryptor.Encrypt(text);
        }

        public async Task<string> Decrypt(string cipherText)
        {
            return await _encryptor.Decrypt(cipherText);
        }

    }
}
