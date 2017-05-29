using System.Threading.Tasks;
using QuizApp.DTOs;
using Refit;

namespace QuizApp.Services
{
	public interface ITriviaServiceClient
	{
		[Get("/api.php?amount={amount}&type=multiple")]
		Task<TriviaRootDTO> GetTriviaQuestions(int amount);
	}
}
