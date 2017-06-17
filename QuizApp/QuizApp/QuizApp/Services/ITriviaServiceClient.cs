using System.Threading.Tasks;
using QuizApp.Core.DTOs;
using Refit;

namespace QuizApp.Core.Services
{
	public interface ITriviaServiceClient
	{
		[Get("/api.php?type=multiple&amount={amount}&category={category}&difficulty={difficulty}&token={token}")]
		Task<TriviaRootDTO> GetTriviaQuestionsAsync(int amount, int category, string difficulty, string token);

		[Get("/api_token.php?command=request")]
		Task<TokenRequestDTO> GetTokenAsync();

		[Get("/api_token.php?command=reset&token={token}")]
		Task<TokenResetDTO> ResetTokenAsync(string token);
	}
}
