using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class IntrumentController : EvaControllerBase<Instrument>
    {
        private readonly IInstrumentService _instrumentService;
        public IntrumentController(IInstrumentService instrumentService) : base(instrumentService)
        {
            _instrumentService = instrumentService;
        }
        [HttpPost]
        public async Task<CustomResultViewModel<string>> ImportFromExcel(string filePath)
        {
            return await _instrumentService.ImportFromExcel(filePath);
        }
    }
}
