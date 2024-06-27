using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;

namespace Eva.Infra.Tools.Extensions
{
    public static class IQueryableExtensions
    {
        private static IQueryable<T> ApplySearchTerm<T>(this IQueryable<T> source, SearchTermViewModel request)
        {
            return !string.IsNullOrWhiteSpace(request.SearchTerm) ? source.Where(t => t.ToString().ToLower().Contains(request.SearchTerm.ToLower())) : source;
        }
        private static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, SortingRequestViewModel request)
        {
            var property = typeof(T).GetProperty(request.SortingItem.Field);
            if (!string.IsNullOrWhiteSpace(request.SortingItem.Field) || property != null)
                return request.SortingItem.SortingType == SortingType.Ascending ? source.OrderBy(t => t.GetType().GetProperty(property.Name).GetValue(t)) : source.OrderByDescending(t => t.GetType().GetProperty(property.Name).GetValue(t));
            else
                return source;
        }
        private static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PaginationRequestViewModel request)
        {
            request.FixPagination();
            return source.Skip((request.PageNumber - 1) * request.RecordsPerPage).Take(request.RecordsPerPage);
        }
        public static IQueryable<T> ApplyBaseRequest<T>(this IQueryable<T> source, BaseRequestViewModel request, out Pagination pagination)
        {
            int totalRecords = source.Count();
            bool hasInvalidPaginationNumber = request.PaginationRequest.PageNumber.IsNullOrZero() || request.PaginationRequest.RecordsPerPage.IsNullOrZero();
            pagination = new Pagination()
            {
                CurrentPage = hasInvalidPaginationNumber ? Pagination.DefaultCurrentPage : request.PaginationRequest.PageNumber,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / (hasInvalidPaginationNumber ? Pagination.DefaultRecordsPerPage : request.PaginationRequest.RecordsPerPage)),
                TotalRecords = totalRecords,
            };
            return source.ApplySearchTerm(request.SearchTermRequest).ApplySorting(request.SortingRequest).ApplyPagination(request.PaginationRequest);
        }
    }
}
