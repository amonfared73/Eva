using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseInterfaces;

namespace Eva.Core.Domain.BaseModels
{
    [ConfigurationRequired]
    public class RsaCryptographyConfiguration : IEvaEntityConfiguration
    {
        public int KeySize { get; set; }
        public string PrivateKey { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
    }
}
