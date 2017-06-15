using System.Threading.Tasks;
using QuizApp.Core.Enums;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface IQuestionsService
	{
		Task<QuestionPO> GetQuestion(int categoryId, QuestionDifficulty questionDifficulty);
		Task WipeMemory();
	}
}
