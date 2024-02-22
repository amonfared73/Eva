using Eva.Core.Domain.Models.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class RsaEncryptor
    {
        private readonly RsaKeyPair _rsaKeyPair;
        public RsaEncryptor(RsaKeyPair rsaKeyPair)
        {
            _rsaKeyPair = rsaKeyPair;
        }
        public string Encrypt(string text)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_rsaKeyPair.PublicKey);

                byte[] plainBytes = Encoding.UTF8.GetBytes(text);
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, false);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
        public string Decrypt(string encryptedText)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_rsaKeyPair.PrivateKey);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
