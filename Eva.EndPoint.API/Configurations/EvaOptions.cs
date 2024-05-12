using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Conventions;

namespace Eva.EndPoint.API.Configurations
{
    public class EvaOptions
    {
        public IConfiguration EvaConfiguration { get; set; }
        public string EvaConnectionString { get; set; }
        public AuthenticationConfiguration EvaAuthenticationConfiguration { get; set; }
        public EvaConventions EvaConventions { get; set; }
        public EvaCachingKeys EvaCachingKeys { get; set; }
    }
}
