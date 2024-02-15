using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.ViewModels
{
    public class EvaLogReportViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string RequestUrl { get; set; } = string.Empty;
        public string RequestMethod { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string? Payload { get; set; }
        public string? Response { get; set; }
    }
}
