using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Authentication : ModelBase
    {
        public static string LoginUrl = "/api/Authentication/Login";
        public static string RegisterUrl = "/api/Authentication/Register";
    }
}
