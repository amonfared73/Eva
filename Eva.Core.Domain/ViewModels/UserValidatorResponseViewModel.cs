using Eva.Core.Domain.Responses;

namespace Eva.Core.Domain.ViewModels
{
    public class UserValidatorResponseViewModel
    {
        public bool State { get; set; }
        public ResponseMessage ResponseMessage { get; set; } = new ResponseMessage(new List<string>());
    }
}
