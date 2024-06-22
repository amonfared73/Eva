using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaBaseService<TModel, TViewModel> where TModel : ModelBase where TViewModel : ViewModelBase
    {
        Task<PagedResultViewModel<TModel>> GetAllAsync(BaseRequestViewModel request);
        Task<SingleResultViewModel<TModel>> GetByIdAsync(int id);
        Task<ActionResultViewModel<TModel>> InsertAsync(TModel entity);
        Task<ActionResultViewModel<TModel>> UpdateAsync(TModel entity);
        Task<ActionResultViewModel<TModel>> DeleteAsync(int id);
        Task<CustomResultViewModel<IEnumerable<TViewModel>>> ImportFromExcel(string filePath);
        Task<CustomResultViewModel<byte[]>> ToByte(int id);
    }
}
