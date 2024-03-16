using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Authentication : ModelBase
    {
        public static string LoginUrl = "/api/Authentication/Login";
        public static string RegisterUrl = "/api/Authentication/Register";
    }
}
