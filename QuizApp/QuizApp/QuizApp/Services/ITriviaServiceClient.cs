using System.Threading.Tasks;
using QuizApp.Core.DTOs;
using Refit;

namespace QuizApp.Core.Services
{
	public interface ITriviaServiceClient
	{
		[Get("/api.php?amount={amount}&type=multiple")]
		Task<TriviaRootDTO> GetTriviaQuestions(int amount);
	}
}
