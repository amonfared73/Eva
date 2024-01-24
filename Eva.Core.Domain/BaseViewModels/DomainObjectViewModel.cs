using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.BaseViewModels
{
    public class DomainObjectViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
