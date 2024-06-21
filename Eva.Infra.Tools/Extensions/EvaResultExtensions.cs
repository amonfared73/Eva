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

        public static async Task<EvaResult<TOut>> BindAsync<TIn, TOut>(this Task<EvaResult<TIn>> result, Func<TIn, Task<EvaResult<TOut>>> bindAsync)
        {
            return result.Result.IsSuccess ?
                await bindAsync(result.Result.Value) :
                EvaResult<TOut>.Failure(result.Result.Error);
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

        public static async Task<EvaResult<TOut>> TryCatchAsync<TIn, TOut>(this Task<EvaResult<TIn>> result, Func<TIn, Task<TOut>> funcAsync, Error error)
        {
            try
            {
                return result.Result.IsSuccess ?
                    EvaResult<TOut>.Success(await funcAsync(result.Result.Value)) :
                    EvaResult<TOut>.Failure(result.Result.Error);
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

        public static async Task<EvaResult<TIn>> TapAsync<TIn>(this Task<EvaResult<TIn>> result, Func<TIn, Task> actionAsync)
        {
            if (result.Result.IsSuccess)
                await actionAsync(result.Result.Value);
            return result.Result;
        }

        public static TOut Match<TIn, TOut>(this EvaResult<TIn> result, Func<TIn, TOut> onSuccess, Func<Error, TOut> onFailure)
        {
            return result.IsSuccess ?
                onSuccess(result.Value) :
                onFailure(result.Error);
        }

        public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<EvaResult<TIn>> result, Func<TIn, Task<TOut>> onSuccessAsync, Func<Error, Task<TOut>> onFailureAsync)
        {
            return result.Result.IsSuccess ?
                await onSuccessAsync(result.Result.Value) :
                await onFailureAsync(result.Result.Error);
        }
    }
}
