using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Blog : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public IEnumerable<Post> Posts { get; set; }
    }
}
