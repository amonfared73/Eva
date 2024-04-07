using Eva.Core.Domain.Enums;

namespace Eva.Core.Domain.Attributes.LifeTimeCycle
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RegistrationRequiredAttribute : Attribute
    {
        public RegistrationType RegistrationType { get; }
        public RegistrationRequiredAttribute(RegistrationType registrationType)
        {
            RegistrationType = registrationType; 
        }
    }
}
