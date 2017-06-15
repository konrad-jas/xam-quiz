using System.Linq;
using System.Net;
using System.Threading.Tasks;
using QuizApp.Core.Enums;
using QuizApp.Core.Extensions;
using QuizApp.Core.POs;

namespace QuizApp.Core.Services.Impl
{
	public class QuestionsService : IQuestionsService
	{
		private readonly ITriviaServiceProxy _triviaServiceProxy;
		private readonly ITokenService _tokenService;

		public QuestionsService(ITriviaServiceProxy triviaServiceProxy, ITokenService tokenService)
		{
			_triviaServiceProxy = triviaServiceProxy;
			_tokenService = tokenService;
		}

		public async Task<QuestionPO> GetQuestion(int categoryId, QuestionDifficulty questionDifficulty)
		{
			var difficulty = questionDifficulty.ToString().ToLower();
			var token = await _tokenService.GetOrCreateToken();

			var questions = await _triviaServiceProxy.GetTriviaQuestions(1, categoryId, difficulty, token);
			var question = questions?.Results?.FirstOrDefault();
			if (question?.IncorrectAnswers == null)
				return null;

			var answers = question.IncorrectAnswers;
			answers.Add(question.CorrectAnswer);
			answers.Shuffle();

			return new QuestionPO
			{
				Question = WebUtility.HtmlDecode(question.Question),
				Answers = answers
			};
		}

		public async Task WipeMemory()
		{
			await _tokenService.ResetToken();
		}
	}
}
