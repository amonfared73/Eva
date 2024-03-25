using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Comment : ModelBase
    {
        public string Text { get; set; } = string.Empty;
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
