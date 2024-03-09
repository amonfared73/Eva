using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class RsaEncryptor
    {
        private readonly RsaCryptographyConfiguration _configuration;
        private readonly RsaParser _parser;
        public RsaEncryptor(RsaCryptographyConfiguration configuration, RsaParser parser)
        {
            _configuration = configuration;
            _parser = parser;
        }
        public string Encrypt(string text)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(_configuration.PublicKey);

                    byte[] plainBytes = Encoding.UTF8.GetBytes(text);
                    byte[] encryptedBytes = rsa.Encrypt(plainBytes, false);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {
                return string.Format("Unable to encrypt, {0}", ex.Message);
            }
        }
        public string Decrypt(string encryptedText)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(_configuration.PrivateKey);

                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception ex)
            {
                return string.Format("Unable to decrypt, {0}", ex.Message);
            }
        }
        public byte[] SignContent(byte[] content)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_parser.ParseRsaParametersFromXml(_configuration.PrivateKey));
                return rsa.SignData(content, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public bool VerifyContent(byte[] content, byte[] signature)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_parser.ParseRsaParametersFromXml(_configuration.PublicKey));
                return rsa.VerifyData(content, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
    }
}
