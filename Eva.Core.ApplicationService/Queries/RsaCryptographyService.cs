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
    public class RsaCryptographyService : BaseService<RsaCryptography, RsaCryptographyViewModel>, IRsaCryptographyService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        private readonly RsaEncryptor _encryptor;
        public RsaCryptographyService(IEvaDbContextFactory contextFactory, RsaEncryptor encryptor) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _encryptor = encryptor;
        }
        public string Encrypt(string text)
        {
            return _encryptor.Encrypt(text);
        }
        public string Decrypt(string encryptedText)
        {
            return _encryptor.Decrypt(encryptedText);
        }

        public bool Verify(string plainText, string encryptedText)
        {
            return plainText == _encryptor.Decrypt(encryptedText);
        }
    }
}
