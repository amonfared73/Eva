using Eva.Core.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Infra.Tools.Extentions
{
    public static class PostCreationViewModelExtensions
    {
        public static bool IsValid(this PostCreationViewModel request)
        {
            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Content) || request.BlogId == 0)
                return false;
            return true;
        }
    }
}
