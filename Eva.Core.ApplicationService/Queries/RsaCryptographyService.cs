using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class RsaCryptographyService : BaseService<RsaCryptography>, IRsaCryptographyService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        private readonly RsaEncryptor _encryptor;
        public RsaCryptographyService(IDbContextFactory<EvaDbContext> contextFactory, RsaEncryptor encryptor) : base(contextFactory)
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

    }
}
