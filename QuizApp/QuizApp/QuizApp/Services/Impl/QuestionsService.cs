using System.Linq;
using System.Threading.Tasks;
using QuizApp.Extensions;
using QuizApp.POs;

namespace QuizApp.Services.Impl
{
	public class QuestionsService : IQuestionsService
	{
		private readonly ITriviaServiceClient _triviaServiceClient;

		public QuestionsService(ITriviaServiceClient triviaServiceClient)
		{
			_triviaServiceClient = triviaServiceClient;
		}

		public async Task<QuestionPO> Get()
		{
			var questions = await _triviaServiceClient.GetTriviaQuestions(1);
			var question = questions?.Results?.FirstOrDefault();
			if (question?.IncorrectAnswers == null)
				return null;

			var answers = question.IncorrectAnswers;
			answers.Add(question.CorrectAnswer);
			answers.Shuffle();

			return new QuestionPO
			{
				Question = question.Question,
				Answers = answers
			};
		}
	}
}
