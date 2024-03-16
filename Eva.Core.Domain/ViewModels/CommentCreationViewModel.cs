using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.ViewModels
{
    public class CommentCreationViewModel
    {
        public string Text { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
