using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IInstrumentService : IBaseService<Instrument>
    {
        Task<CustomResultViewModel<string>> ImportFromExcel(string filePath);
    }
}
