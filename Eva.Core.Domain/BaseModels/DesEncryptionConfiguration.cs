using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseInterfaces;

namespace Eva.Core.Domain.BaseModels
{
    [ConfigurationRequired]
    public class DesEncryptionConfiguration : SymmetricEncryptionConfiguration, IEvaEntityConfiguration
    {

    }
}
