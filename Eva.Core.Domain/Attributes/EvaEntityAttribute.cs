namespace Eva.Core.Domain.Attributes
{
    /// <summary>
    /// Classes decorated with this attribute are considered the <see href="https://github.com/amonfared73/Eva">Eva</see> main business domain classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EvaEntityAttribute : Attribute
    {

    }
}
