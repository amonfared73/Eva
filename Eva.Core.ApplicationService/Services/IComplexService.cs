using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IComplexService : IBaseService<Complex, ComplexViewModel>
    {
        Task<ActionResultViewModel<Complex>> AddNewComplexNumberAsync(ComplexNumberDto complexNumberDto);
    }
}
