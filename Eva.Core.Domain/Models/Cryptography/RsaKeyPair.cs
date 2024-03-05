using Eva.Core.Domain.BaseModels;
using System.Security.Cryptography;

namespace Eva.Core.Domain.Models.Cryptography
{
    public class RsaKeyPair
    {
        private readonly RsaCryptographyConfiguration _configuration;
        /// <summary>
        /// This class used to hold RSA Cryptography configurations from its respective class and appsettings.json file
        /// I'll delete it later
        /// </summary>
        /// <param name="configuration"></param>
        public RsaKeyPair(RsaCryptographyConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string PublicKey { get; } = string.Empty;
        public string PrivateKey { get; } = string.Empty;
        public RsaKeyPair()
        {
            using (var rsa = new RSACryptoServiceProvider(_configuration.KeySize))
            {
                PublicKey = _configuration.PublicKey;
                PrivateKey = _configuration.PrivateKey;
            }
        }
    }
}
