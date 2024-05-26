using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Conventions;

namespace Eva.EndPoint.API.Configurations
{
    /// <summary>
    /// Holds options for configuring <see href="https://github.com/amonfared73/Eva">Eva</see> framework
    /// </summary>
    public class EvaOptions
    {
        /// <summary>
        /// Represents a set of key/value application configuration properties
        /// </summary>
        public IConfiguration EvaConfiguration { get; set; }
        /// <summary>
        /// Represents <see href="https://github.com/amonfared73/Eva">Eva</see> database connection string
        /// </summary>
        public string EvaConnectionString { get; set; }
        /// <summary>
        /// Represents <see href="https://github.com/amonfared73/Eva">Eva</see> <see cref="AuthenticationConfiguration"/> for configuring JWT tokens
        /// </summary>
        public AuthenticationConfiguration EvaAuthenticationConfiguration { get; set; }
        /// <summary>
        /// Represents controller conventions.
        /// Allows customization of the <see cref="ApplicationModel"/>, <see cref="ControllerModel"/>, <see cref="ActionModel"/> and <see cref="ParameterModel"/>
        /// </summary>
        public EvaConventions EvaConventions { get; set; }
        /// <summary>
        /// Represent <see href="https://github.com/amonfared73/Eva">Eva</see> caching keys to store in cache
        /// </summary>
        public EvaCachingKeys EvaCachingKeys { get; set; }
        /// <summary>
        /// Represents the external APIs consumed in <see href="https://github.com/amonfared73/Eva">Eva</see>
        /// </summary>
        public ExternalServicesUri ExternalServicesUri { get; set; }
    }
}
