using Eva.Core.Domain.BaseModels;
using System.Security.Cryptography;
using System.Text;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class AesEncryptor
    {
        private readonly AesEncryptionConfiguration _configuration;
        public AesEncryptor(AesEncryptionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> Encrypt(string text)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_configuration.Key);
                    aes.IV = Encoding.UTF8.GetBytes(_configuration.IV);

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
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
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_configuration.Key);
                    aes.IV = Encoding.UTF8.GetBytes(_configuration.IV);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
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
