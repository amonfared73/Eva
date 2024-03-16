using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    public class Blog : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        [JsonIgnore]
        public IEnumerable<Post> Posts { get; set; }
    }
}
