using Eva.Core.Domain.Responses;

namespace Eva.Core.Domain.BaseViewModels
{
    public class CustomActionResultViewModel<T>
    {
        public T? Entity { get; set; }
        public bool HasError { get; set; } = false;
        public ResponseMessage ResponseMessage { get; set; }
    }
}
