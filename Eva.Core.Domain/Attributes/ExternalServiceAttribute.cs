namespace Eva.Core.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExternalServiceAttribute : Attribute
    {
        public Type Type { get; }
        public ExternalServiceAttribute(Type type)
        {
            Type = type;
        }
    }
}
