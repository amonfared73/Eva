using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.BaseViewModels
{
    public class ActionResultViewModel<T> where T : DomainObjectModel
    {
        public T? Entity { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
}
