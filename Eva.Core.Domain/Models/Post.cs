using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Post : ModelBase
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
