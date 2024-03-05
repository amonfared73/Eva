using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class RsaEncryptor
    {
        private readonly RsaCryptographyConfiguration _configuration;
        public RsaEncryptor(RsaCryptographyConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}
