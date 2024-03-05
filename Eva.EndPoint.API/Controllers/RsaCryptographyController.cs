using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    
    public class RsaCryptographyController : EvaControllerBase<RsaCryptography>
    {
        private readonly IRsaCryptographyService _service;
        /// <summary>
        /// This controller provides encryption and decryption of texts by means of RSA Cryptography
        /// This encryption currently works in one session and an encrypted text will not be decrypted in an other session
        /// I'm working on it :)
        /// </summary>
        /// <param name="service"></param>
        public RsaCryptographyController(IRsaCryptographyService service) : base(service)
        {
            _service = service;
        }
        [HttpPost]
        public string Encrypt(string text)
        {
            return _service.Encrypt(text);
        }
        [HttpPost]
        public string Decrypt(string encryptedText)
        {
            return _service.Decrypt(encryptedText);
        }
    }
}
