using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IBaseService<T> where T : DomainObject
    {
        Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request);
        Task<SingleResultViewModel<T>> GetByIdAsync(int id);
        Task<SingleResultViewModel<T>> InsertAsync(T entity);
        Task<SingleResultViewModel<T>> UpdateAsync(T entity);
        Task<SingleResultViewModel<T>> DeleteAsync(int id);
    }
}
