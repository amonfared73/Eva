using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;

namespace Eva.EndPoint.API.Controllers.Inv
{
    public class TransactionController : EvaControllerBase<Transaction, TransactionViewModel>
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService) : base(transactionService)
        {
            _transactionService = transactionService;
        }
    }
}
