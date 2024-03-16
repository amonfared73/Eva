using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class IntrumentController : EvaControllerBase<Instrument, InstrumentViewModel>
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
