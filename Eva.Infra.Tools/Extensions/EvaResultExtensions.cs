using Eva.Core.Domain.BaseModels;

namespace Eva.Infra.Tools.Extensions
{
    public static class EvaResultExtensions
    {
        public static EvaResult<TOut> Bind<TIn, TOut>(this EvaResult<TIn> result, Func<TIn, EvaResult<TOut>> bind)
        {
            return result.IsSuccess ?
                bind(result.Value) :
                EvaResult<TOut>.Failure(result.Error);
        }

        public static EvaResult<TOut> TryCatch<TIn, TOut>(this EvaResult<TIn> result, Func<TIn, TOut> func, Error error)
        {
            try
            {
                return result.IsSuccess ?
                    EvaResult<TOut>.Success(func(result.Value)) :
                    EvaResult<TOut>.Failure(result.Error);
            }
            catch
            {
                return EvaResult<TOut>.Failure(error);
            }
        }

        public static EvaResult<TIn> Tap<TIn>(this EvaResult<TIn> result, Action<TIn> action)
        {
            if (result.IsSuccess)
                action(result.Value);
            return result;
        }

        public static async Task<EvaResult<TIn>> TapAsync<TIn>(this EvaResult<TIn> result, Func<TIn, Task> actionAsync)
        {
            if (result.IsSuccess)
                await actionAsync(result.Value);
            return result;
        }

        public static TOut Match<TIn, TOut>(this EvaResult<TIn> result, Func<TIn, TOut> onSuccess, Func<Error, TOut> onFailure)
        {
            return result.IsSuccess ?
                onSuccess(result.Value) :
                onFailure(result.Error);
        }
    }
}
