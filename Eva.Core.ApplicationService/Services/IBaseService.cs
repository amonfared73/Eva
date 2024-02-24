using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IBaseService<T> where T : DomainObject
    {
        Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request);
        Task<SingleResultViewModel<T>> GetByIdAsync(int id);
        Task<ActionResultViewModel<T>> InsertAsync(T entity);
        Task<ActionResultViewModel<T>> UpdateAsync(T entity);
        Task<ActionResultViewModel<T>> DeleteAsync(int id);
        Task<CustomActionResultViewModel<byte[]>> ToByte(int id);
    }
}
