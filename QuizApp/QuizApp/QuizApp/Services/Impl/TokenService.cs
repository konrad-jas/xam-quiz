using System.Threading.Tasks;

namespace QuizApp.Core.Services.Impl
{
	public class TokenService : ITokenService
	{
		private readonly ITriviaServiceProxy _triviaServiceProxy;
		private string _token;

		public TokenService(ITriviaServiceProxy triviaServiceProxy)
		{
			_triviaServiceProxy = triviaServiceProxy;
		}

		public async Task<string> GetOrCreateTokenAsync()
		{
			if (_token == null)
			{
				var res = await _triviaServiceProxy.GetTokenAsync();
				if (res == null)
					return null;

				_token = res.Token;
			}

			return _token;
		}

		public async Task ResetTokenAsync()
		{
			if (_token == null)
				return;

			await _triviaServiceProxy.ResetTokenAsync(_token);
		}
	}
}
