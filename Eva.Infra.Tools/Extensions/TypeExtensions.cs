using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Enums;
using System.Reflection;

namespace Eva.Infra.Tools.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSingleton(this Type type)
        {
            return type.GetCustomAttribute<RegistrationRequiredAttribute>().RegistrationType == RegistrationType.Singleton;
        }
        public static bool IsTransient(this Type type)
        {
            return type.GetCustomAttribute<RegistrationRequiredAttribute>().RegistrationType == RegistrationType.Transient;
        }
        public static bool IsScoped(this Type type)
        {
            return type.GetCustomAttribute<RegistrationRequiredAttribute>().RegistrationType == RegistrationType.Scoped;
        }
    }
}
