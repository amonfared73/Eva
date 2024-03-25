using Eva.Core.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Infra.Tools.Extentions
{
    public static class CommentCreationViewModelExtensions
    {
        public static bool IsValid(this CommentCreationViewModel request)
        {
            return (!string.IsNullOrEmpty(request.Text) || request.PostId != 0);
        }
    }
}
