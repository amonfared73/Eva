﻿using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;

namespace Eva.Infra.Tools.Extensions
{
    public static class IEnumerableExtensions
    {
        private static IEnumerable<T> ApplySearchTerm<T>(this IEnumerable<T> source, SearchTermViewModel request)
        {
            return !string.IsNullOrWhiteSpace(request.SearchTerm) ? source.Where(t => t.ToString().ToLower().Contains(request.SearchTerm.ToLower())) : source;
        }
        private static IEnumerable<T> ApplySorting<T>(this IEnumerable<T> source, SortingRequestViewModel request)
        {
            var property = typeof(T).GetProperty(request.SortingItem.Field);
            if (!string.IsNullOrWhiteSpace(request.SortingItem.Field) || property != null)
                return request.SortingItem.SortingType == SortingType.Ascending ? source.OrderBy(t => t.GetType().GetProperty(property.Name).GetValue(t)) : source.OrderByDescending(t => t.GetType().GetProperty(property.Name).GetValue(t));
            else
                return source;
        }
        private static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> source, PaginationRequestViewModel request)
        {
            request.FixPagination();
            return source.Skip((request.PageNumber - 1) * request.RecordsPerPage).Take(request.RecordsPerPage);
        }
        public static IEnumerable<T> ApplyBaseRequest<T>(this IEnumerable<T> source, BaseRequestViewModel request, out Pagination pagination)
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
        public static bool HasDuplicates<T>(this IEnumerable<T> source)
        {
            HashSet<T> knwonElements = new();
            foreach (var element in source)
            {
                if (!knwonElements.Add(element))
                    return true;
            }
            return false;
        }

        public static async Task<bool> HasMember<T>(this IEnumerable<T> source)
        {
            return await Task.FromResult(source.Any());
        }
    }
}
