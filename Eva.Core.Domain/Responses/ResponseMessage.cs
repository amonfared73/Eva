namespace Eva.Core.Domain.Responses
{
    public class ResponseMessage
    {
        public IEnumerable<string> Messages { get; set; }
        public ResponseMessage(string Message) : this(new List<string>() { Message }) { }
        public ResponseMessage(IEnumerable<string> messages)
        {
            Messages = messages;
        }
    }
}
