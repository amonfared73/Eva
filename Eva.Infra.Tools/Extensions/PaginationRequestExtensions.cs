using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Infra.Tools.Extensions
{
    public static class PaginationRequestExtensions
    {
        public static PaginationRequestViewModel FixPagination(this PaginationRequestViewModel model)
        {
            if (model.PageNumber == 0 || model.RecordsPerPage == 0)
            {
                model.PageNumber = 1;
                model.RecordsPerPage = 5;
            }
            return model;
        }

        public static Pagination ToPagination(this PaginationRequestViewModel request, int totalRecords)
        {
            return new Pagination()
            {
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / request.RecordsPerPage),
                TotalRecords = totalRecords,
            };
        }
    }
}
