using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Blog : ModelBase
    {
        public string Title { get; set; } = string.Empty;
        public IEnumerable<Post> Posts { get; set; }
    }
}
