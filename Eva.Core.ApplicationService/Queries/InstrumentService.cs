using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using OfficeOpenXml;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class InstrumentService : BaseService<Instrument>, IInstrumentService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public InstrumentService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<CustomResultViewModel<string>> ImportFromExcel(string filePath)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var instruments = new List<Instrument>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var instrument = new Instrument()
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString()
                        };

                        await context.Instruments.AddAsync(instrument);
                    }
                    await context.SaveChangesAsync();
                    return new CustomResultViewModel<string>()
                    {
                        Entity = null,
                        HasError = false,
                        ResponseMessage = new Domain.Responses.ResponseMessage("Import from excel done successfully")
                    };
                }


            }
        }
    }
}
