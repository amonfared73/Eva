using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models.General
{
    [EvaEntity]
    public class Good : ModelBase
    {
        public string GoodCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
