using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.ViewModels
{
    public class EvaLogReportOutputViewModel : ModelBase
    {
        public string Username { get; set; } = string.Empty;
        public string RequestUrl { get; set; } = string.Empty;
        public string RequestMethod { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string? Payload { get; set; }
        public string? Response { get; set; }
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", Username, RequestUrl, RequestMethod, StatusCode, Payload, Response);
        }
    }
}
