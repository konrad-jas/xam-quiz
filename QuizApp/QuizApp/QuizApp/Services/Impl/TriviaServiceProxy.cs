using System;
using System.Threading.Tasks;
using MvvmCross.Platform.Platform;
using QuizApp.Core.DTOs;

namespace QuizApp.Core.Services.Impl
{
	public class TriviaServiceProxy : ITriviaServiceProxy
	{
		private readonly ITriviaServiceClient _triviaServiceClient;

		public TriviaServiceProxy(ITriviaServiceClient triviaServiceClient)
		{
			_triviaServiceClient = triviaServiceClient;
		}

		public async Task<TriviaRootDTO> GetTriviaQuestions(int amount, int category, string difficulty, string token)
		{
			return await FetchAsync(async () => await _triviaServiceClient.GetTriviaQuestions(amount, category, difficulty, token),
				() => null).ConfigureAwait(false);
		}

		public async Task<TokenRequestDTO> GetToken()
		{
			return await FetchAsync(async () => await _triviaServiceClient.GetToken(),
				() => null).ConfigureAwait(false);
		}

		public async Task<TokenResetDTO> ResetToken(string token)
		{
			return await FetchAsync(async () => await _triviaServiceClient.ResetToken(token),
				() => null).ConfigureAwait(false);
		}

		private async Task<T> FetchAsync<T>(Func<Task<T>> fetchFunc, Func<T> fallbackFunc)
		{
			try
			{
				return await fetchFunc().ConfigureAwait(false);
			}
			catch (Exception e)
			{
				MvxTrace.TaggedError("trivia service", $"{e}");
			}

			return fallbackFunc();
		}
	}
}
