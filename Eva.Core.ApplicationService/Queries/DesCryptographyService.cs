using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Enums;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class DesCryptographyService : BaseService<DesCryptography, DesCryptographyViewModel>, IDesCryptographyService
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
