using System.Linq;
using System.Threading.Tasks;
using QuizApp.Core.Enums;
using QuizApp.Core.POs;
using System.Collections.Generic;
using AutoMapper;
using QuizApp.Core.DTOs;

namespace QuizApp.Core.Services.Impl
{
	public class QuestionsService : IQuestionsService
	{
		private readonly ITriviaServiceProxy _triviaServiceProxy;
		private readonly ITokenService _tokenService;

		private readonly IDictionary<QuestionDifficulty, int?> _categoryIds;
		private readonly IDictionary<QuestionDifficulty, IList<QuestionPO>> _questions;

		public QuestionsService(ITriviaServiceProxy triviaServiceProxy, ITokenService tokenService)
		{
			_triviaServiceProxy = triviaServiceProxy;
			_tokenService = tokenService;

			_categoryIds = new Dictionary<QuestionDifficulty, int?>();
			_questions = new Dictionary<QuestionDifficulty, IList<QuestionPO>>();
		}

		public async Task<QuestionPO> GetQuestionAsync(int categoryId, QuestionDifficulty difficulty)
		{
			var sameCategory = _categoryIds.ContainsKey(difficulty) && _categoryIds[difficulty] == categoryId;
			var anyQuestions = _questions.ContainsKey(difficulty) && _questions[difficulty].Any();

			if (!sameCategory || !anyQuestions)
			{
				await PrefetchQuestions(categoryId, difficulty);
			}

			var question = _questions[difficulty].First();
			_questions[difficulty].RemoveAt(0);
			return question;
		}

		public async Task WipeMemoryAsync()
		{
			await _tokenService.ResetTokenAsync();
		}

		private async Task PrefetchQuestions(int categoryId, QuestionDifficulty difficulty)
		{
			var questions = await FetchQuestions(10, categoryId, difficulty);
			_questions[difficulty] = questions.Select(Mapper.Map<QuestionPO>).ToList();
			_categoryIds[difficulty] = categoryId;
		}

		private async Task<IEnumerable<TriviaQuestionDTO>> FetchQuestions(int amount, int categoryId, QuestionDifficulty questionDifficulty)
		{
			var difficulty = questionDifficulty.ToString().ToLower();
			var token = await _tokenService.GetOrCreateTokenAsync();

			var triviaQuestions = await _triviaServiceProxy.GetTriviaQuestionsAsync(amount, categoryId, difficulty, token);
			return triviaQuestions?.Results;
		}
	}
}
