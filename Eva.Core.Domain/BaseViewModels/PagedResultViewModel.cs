using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Responses;

namespace Eva.Core.Domain.BaseViewModels
{
    public class PagedResultViewModel<T> where T : ModelBase
    {
        public Pagination Pagination { get; set; }
        public IQueryable<T> Data { get; set; }
        public bool HasError { get; set; } = false;
        public ResponseMessage ResponseMessage { get; set; }
    }
}
