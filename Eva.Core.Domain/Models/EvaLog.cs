using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class EvaLog : ModelBase
    {
        public string LogTypeCode { get; set; } = string.Empty;
        public string RequestUrl { get; set; } = string.Empty;
        public string RequestMethod { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string? Payload { get; set; }
        public string? Response { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public static string SensitiveCredentials = $"Sensitive Credentials";
    }
}
