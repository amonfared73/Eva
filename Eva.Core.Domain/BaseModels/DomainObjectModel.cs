using Eva.Core.Domain.BaseViewModels;

namespace Eva.Core.Domain.BaseModels
{
    public class DomainObjectModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public static implicit operator DomainObjectViewModel(DomainObjectModel domainObjectModel)
        {
            return new DomainObjectViewModel()
            {
                Id = domainObjectModel.Id,
                CreatedOn = domainObjectModel.CreatedOn,
            };
        }
    }
}
