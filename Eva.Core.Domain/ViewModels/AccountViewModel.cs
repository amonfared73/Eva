using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.Domain.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public IEnumerable<AccountViewModel> ChildAccounts { get; set; }
    }
}
