using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    public class EvaLog : DomainObject
    {
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string StatusCode { get; set; }
        public string? Payload { get; set; }
        public string? Response { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
