using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class ComplexController : EvaControllerBase<Complex>
    {
        private readonly IComplexService _service;

        public ComplexController(IComplexService service) : base(service) 
        {
            _service = service;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Complex>> InsertAsync(Complex entity)
        {
            return base.InsertAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Complex>> AddNewComplexNumberAsync(ComplexNumberDto complexNumberDto)
        {
            return await _service.AddNewComplexNumberAsync(complexNumberDto);
        }
    }
}
