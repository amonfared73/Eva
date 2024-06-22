using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class ComplexService : EvaBaseService<Complex, ComplexViewModel>, IComplexService
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

        public async Task<ActionResultViewModel<Complex>> UpdateComplexNumberAsync(UpdateComplexNumberDto complexNumberDto)
        {
            using(EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var complexNumber = await context.ComplexNumbers.FirstOrDefaultAsync(c => c.Id == complexNumberDto.Id);
                if (complexNumber is null)
                    throw new EvaNotFoundException($"Complex number not found", typeof(Complex));

                complexNumber.Real = complexNumberDto.Real;
                complexNumber.Imaginary = complexNumberDto.Imaginary;
                complexNumber.FriendlyState = complexNumber.ToString();

                await context.SaveChangesAsync();
                return new ActionResultViewModel<Complex>()
                {
                    Entity = complexNumber,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Complex number updated successfully")
                };
            }
        }
    }
}
