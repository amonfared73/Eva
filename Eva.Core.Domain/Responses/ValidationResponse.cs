namespace Eva.Core.Domain.Responses
{
    /// <summary>
    /// The default response of validation process, consists of a boolean parameter to checks its validity and a <see cref="ResponseMessage"/> to hold respective messages
    /// </summary>
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
}
