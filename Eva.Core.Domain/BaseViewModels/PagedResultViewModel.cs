using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.BaseViewModels
{
    public class PagedResultViewModel<T> where T : DomainObject
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
        public bool HasError { get; set; } = false;
    }
}
