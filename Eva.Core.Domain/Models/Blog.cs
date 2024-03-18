using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    public class Blog : ModelBase
    {
        public string Title { get; set; } = string.Empty;
        public IEnumerable<Post> Posts { get; set; }
    }
}
