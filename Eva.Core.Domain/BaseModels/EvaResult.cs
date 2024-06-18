namespace Eva.Core.Domain.BaseModels
{
    /// <summary>
    /// This class is used to implement Railway Oriented Programming paradigm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EvaResult<T>
    {
        private EvaResult(T value)
        {
            Value = value;
            IsSuccess = true;
        }
        private EvaResult(Error error)
        {
            Error = error;
            IsSuccess = false;
        }
        public T Value { get; }
        public Error Error { get; }
        public bool IsSuccess { get; private set; }
        public static EvaResult<T> Success(T value) => new(value);
        public static EvaResult<T> Failure(Error error) => new(error);
    }
}
