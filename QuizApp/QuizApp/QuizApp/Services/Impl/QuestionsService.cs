using System.Linq;
using System.Net;
using System.Threading.Tasks;
using QuizApp.Core.Enums;
using QuizApp.Core.Extensions;
using QuizApp.Core.POs;
using System.Collections.Generic;
using QuizApp.Core.DTOs;

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
			var triviaQuestions = await FetchQuestions(1, categoryId, questionDifficulty);
			var triviaQuestion = triviaQuestions?.FirstOrDefault();
			if (triviaQuestion?.IncorrectAnswers == null)
				return null;

			var answers = triviaQuestion.IncorrectAnswers.Select(x => new AnswerPO { Answer = WebUtility.HtmlDecode(x) }).ToList();
			answers.Add(new AnswerPO { Answer = WebUtility.HtmlDecode(triviaQuestion.CorrectAnswer), Correct = true });
			answers.Shuffle();

			return new QuestionPO
			{
				Question = WebUtility.HtmlDecode(triviaQuestion.Question),
				Answers = answers
			};
		}

		public async Task WipeMemory()
		{
			await _tokenService.ResetToken();
		}

		private async Task<IEnumerable<TriviaQuestionDTO>> FetchQuestions(int amount, int categoryId, QuestionDifficulty questionDifficulty)
		{
			var difficulty = questionDifficulty.ToString().ToLower();
			var token = await _tokenService.GetOrCreateToken();

			var triviaQuestions = await _triviaServiceProxy.GetTriviaQuestions(1, categoryId, difficulty, token);
			if (triviaQuestions == null)
				return null;

			return triviaQuestions.Results;
		}
	}
}
