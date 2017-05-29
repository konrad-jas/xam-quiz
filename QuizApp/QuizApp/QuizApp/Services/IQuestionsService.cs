using System.Threading.Tasks;
using QuizApp.POs;

namespace QuizApp.Services
{
	public interface IQuestionsService
	{
		Task<QuestionPO> Get();
	}
}
