using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class AesCryptographyController : EvaControllerBase<AesCryptography>
    {
        private readonly IAesCryptographyService _service;
        public AesCryptographyController(IAesCryptographyService service) : base(service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<string> Encrypt(string text)
        {
            return await _service.Encrypt(text);
        }
        [HttpPost]
        public async Task<string> Decrypt(string text)
        {
            return await _service.Decrypt(text);
        }
    }
}
