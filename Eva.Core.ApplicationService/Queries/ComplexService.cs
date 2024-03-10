using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class ComplexService : BaseService<Complex>, IComplexService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public ComplexService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ActionResultViewModel<Complex>> AddNewComplexNumberAsync(ComplexNumberDto complexNumberDto)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                if (complexNumberDto == null)
                    throw new CrudException<Complex>(string.Format("Sorry, can not insert null {0} entity", typeof(Complex).Name), BaseOperations.Insert);
                var complex = new Complex()
                {
                    Real = complexNumberDto.Real,
                    Imaginary = complexNumberDto.Imaginary,
                };
                complex.FriendlyState = complex.ToString();
                await context.ComplexNumbers.AddAsync(complex);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Complex>()
                {
                    Entity = complex,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage($"Complex number created successfully, {complex.ToString()}")
                };
            }
        }
    }
}
