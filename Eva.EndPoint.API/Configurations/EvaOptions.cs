using Eva.Core.Domain.BaseModels;

namespace Eva.EndPoint.API.Configurations
{
    public class EvaOptions
    {
        public IConfiguration EvaConfiguration { get; set; }
        public string ConnectionString { get; set; }
        public AuthenticationConfiguration AuthenticationConfiguration { get; set; }
    }
}
