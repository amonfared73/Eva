using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries.Inv
{
    [RegistrationRequired]
    public class TransactionService : BaseService<Transaction, TransactionViewModel>, ITransactionService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public TransactionService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
