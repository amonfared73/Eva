using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EvaControllerBase<TModel, TViewModel> : ControllerBase where TModel : ModelBase where TViewModel : ViewModelBase
    {
        private readonly IEvaBaseService<TModel, TViewModel> _baseService;
        public EvaControllerBase(IEvaBaseService<TModel, TViewModel> baseService)
        {
            _baseService = baseService;
        }

        [HttpPost]
        public virtual async Task<PagedResultViewModel<TModel>> GetAllAsync(BaseRequestViewModel request)
        {
            return await _baseService.GetAllAsync(request);
        }
        [HttpGet("{id}")]
        public virtual async Task<SingleResultViewModel<TModel>> GetByIdAsync(int id)
        {
            try
            {
                return await _baseService.GetByIdAsync(id);
            }
            catch (CrudException<TModel> ex)
            {
                return new SingleResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new SingleResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost]
        public virtual async Task<ActionResultViewModel<TModel>> InsertAsync(TModel entity)
        {
            try
            {
                return await _baseService.InsertAsync(entity);
            }
            catch (CrudException<TModel> ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPut]
        public virtual async Task<ActionResultViewModel<TModel>> UpdateAsync(TModel entity)
        {
            try
            {
                return await _baseService.UpdateAsync(entity);
            }
            catch (CrudException<TModel> ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResultViewModel<TModel>> DeleteAsync(int id)
        {
            try
            {
                return await _baseService.DeleteAsync(id);
            }
            catch (CrudException<TModel> ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<TModel>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost("{id}")]
        public virtual async Task<CustomResultViewModel<byte[]>> ToByteAsync(int id)
        {
            try
            {
                return await _baseService.ToByte(id);
            }
            catch (CrudException<TModel> ex)
            {
                return new CustomResultViewModel<byte[]>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new CustomResultViewModel<byte[]>()
                {
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
    }
}
