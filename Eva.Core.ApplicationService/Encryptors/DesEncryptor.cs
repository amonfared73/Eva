using Eva.Core.Domain.BaseModels;
using Eva.Infra.Tools.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class DesEncryptor
    {
        private readonly DesEncryptionConfiguration _configuration;
        public DesEncryptor(DesEncryptionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> Encrypt(string text)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = Encoding.UTF8.GetBytes(_configuration.Key);
                    des.IV = Encoding.UTF8.GetBytes(_configuration.Key);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                await streamWriter.WriteAsync(text);
                            }
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Format("Unable to encrypt, {0}", ex.Message);
            }
        }
        public async Task<string> Decrypt(string cipherText)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = Encoding.UTF8.GetBytes(_configuration.Key);
                    des.IV = Encoding.UTF8.GetBytes(_configuration.Key);

                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return await streamReader.ReadToEndAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Format("Unable to decrypt, {0}", ex.Message);
            }
        }
    }
}
