using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class ComplexController : EvaControllerBase<Complex, ComplexViewModel>
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
        [NonAction]
        public override Task<ActionResultViewModel<Complex>> UpdateAsync(Complex entity)
        {
            return base.UpdateAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Complex>> AddNewComplexNumberAsync(ComplexNumberDto complexNumberDto)
        {
            try
            {
                return await _service.AddNewComplexNumberAsync(complexNumberDto);
            }
            catch (CrudException<Complex> ex)
            {
                return new ActionResultViewModel<Complex>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Complex>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPut]
        public async Task<ActionResultViewModel<Complex>> UpdateComplexNumberAsync(UpdateComplexNumberDto complexNumberDto)
        {
            try
            {
                return await _service.UpdateComplexNumberAsync(complexNumberDto);
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<Complex>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Complex>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }  
        }
    }
}
