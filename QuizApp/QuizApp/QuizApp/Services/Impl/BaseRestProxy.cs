using System;
using System.Threading.Tasks;
using MvvmCross.Platform.Platform;

namespace QuizApp.Core.Services.Impl
{
	public abstract class BaseRestProxy
	{
		protected virtual async Task<T> FetchAsync<T>(Func<Task<T>> fetchFunc, Func<T> fallbackFunc)
		{
			try
			{
				return await fetchFunc().ConfigureAwait(false);
			}
			catch (Exception e)
			{
				MvxTrace.TaggedError(GetType().Name, $"{e}");
			}

			return fallbackFunc();
		}
	}
}