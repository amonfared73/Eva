namespace Eva.Core.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
