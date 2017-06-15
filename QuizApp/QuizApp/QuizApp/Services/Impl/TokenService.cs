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

		public async Task<string> GetOrCreateToken()
		{
			if (_token == null)
			{
				var res = await _triviaServiceProxy.GetToken();
				if (res == null)
					return null;

				_token = res.Token;
			}

			return _token;
		}

		public async Task ResetToken()
		{
			if (_token == null)
				return;

			await _triviaServiceProxy.ResetToken(_token);
		}
	}
}
