﻿using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IInstrumentService : IBaseService<Instrument, InstrumentViewModel>
    {
        Task<CustomResultViewModel<string>> ImportFromExcel(string filePath);
    }
}
