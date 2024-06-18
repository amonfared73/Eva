using Eva.Core.Domain.BaseModels;

namespace Eva.Infra.Tools.Extensions
{
    public static class ResultExtensions
    {
        public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
        {
            return result.IsSuccess ?
                bind(result.Value) :
                Result<TOut>.Failure(result.Error);
        }

        public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func, Error error)
        {
            try
            {
                return result.IsSuccess ?
                    Result<TOut>.Success(func(result.Value)) :
                    Result<TOut>.Failure(result.Error);
            }
            catch
            {
                return Result<TOut>.Failure(error);
            }
        }

        public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
        {
            if (result.IsSuccess)
                action(result.Value);
            return result;
        }

        public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> onSuccess, Func<Error, TOut> onFailure)
        {
            return result.IsSuccess ?
                onSuccess(result.Value) :
                onFailure(result.Error);
        }
    }
}
