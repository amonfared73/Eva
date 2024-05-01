namespace Eva.Core.Domain.Attributes
{
    /// <summary>
    /// Controller action methods decorated with this attribute are still involved in logging mechanism, but their request payloads and responses are not exposed due to security concerns
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreLoggingAttribute : Attribute
    {

    }
}
