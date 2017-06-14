using System.Threading.Tasks;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services
{
	public interface IQuestionsService
	{
		Task<QuestionPO> Get();
	}
}
