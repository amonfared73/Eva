using Eva.Core.Domain.Enums;

namespace Eva.Core.Domain.Exceptions
{
    public class CrudException<T> : Exception
    {
        public BaseOperations BaseOperation { get; }

        public CrudException(BaseOperations baseOperation)
        {
            BaseOperation = baseOperation;
        }

        public CrudException(string? message, BaseOperations baseOperation) : base(message)
        {
            BaseOperation = baseOperation;
        }

        public CrudException(string? message, Exception? innerException, BaseOperations baseOperation) : base(message, innerException)
        {
            BaseOperation = baseOperation;
        }
    }
}
