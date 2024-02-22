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
