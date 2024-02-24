namespace Eva.Core.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExternalServiceAttribute : Attribute
    {
        public string Name { get; }

        public ExternalServiceAttribute(string name)
        {
            Name = name;
        }
    }
}
