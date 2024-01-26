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
    public class EvaControllerBase<T> : ControllerBase where T : DomainObject
    {
        private readonly IBaseService<T> _baseService;

        public EvaControllerBase(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        [HttpPost]
        public virtual async Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request)
        {
            return await _baseService.GetAllAsync(request);
        }
        [HttpGet("{id}")]
        public virtual async Task<SingleResultViewModel<T>> GetByIdAsync(int id)
        {
            try
            {
                return await _baseService.GetByIdAsync(id);
            }
            catch (CrudException<T> ex)
            {
                return new SingleResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new SingleResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost]
        public virtual async Task<ActionResultViewModel<T>> InsertAsync(T entity)
        {
            try
            {
                return await _baseService.InsertAsync(entity);
            }
            catch (CrudException<T> ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPut]
        public virtual async Task<ActionResultViewModel<T>> UpdateAsync(T entity)
        {
            try
            {
                return await _baseService.UpdateAsync(entity);
            }
            catch (CrudException<T> ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResultViewModel<T>> DeleteAsync(int id)
        {
            try
            {
                return await _baseService.DeleteAsync(id);
            }
            catch (CrudException<T> ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<T>()
                {
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
    }
}
