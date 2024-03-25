using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Cryptography;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]

    public class RsaCryptographyController : EvaControllerBase<RsaCryptography, RsaCryptographyViewModel>
    {
        private readonly IRsaCryptographyService _service;
        /// <summary>
        /// This controller provides encryption and decryption of texts by means of RSA Cryptography
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
