using System.Threading.Tasks;
using QuizApp.Core.Enums;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface IQuestionsService
	{
		Task<QuestionPO> GetQuestionAsync(int categoryId, QuestionDifficulty difficulty);
		Task WipeMemoryAsync();
	}
}
