﻿using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;

namespace Eva.Infra.Tools.Extensions
{
    public static class BaseViewModelExtensions
    {
        public static PagedResultViewModel<T> ToPagedResultViewModelAsync<T>(this IQueryable<T> source, BaseRequestViewModel request) where T : ModelBase
        {
            return new PagedResultViewModel<T>()
            {
                Data = source.ApplyBaseRequest(request, out Pagination pagination),
                Pagination = pagination
            };
        }
    }
}
