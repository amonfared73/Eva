﻿using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Responses;

namespace Eva.Core.Domain.BaseViewModels
{
    public class ActionResultViewModel<T> where T : DomainObject
    {
        public T? Entity { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
}