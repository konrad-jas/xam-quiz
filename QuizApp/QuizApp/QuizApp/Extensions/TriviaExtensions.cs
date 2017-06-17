using System.Linq;
using System.Net;
using QuizApp.Core.DTOs;
using QuizApp.Core.POs;

namespace QuizApp.Core.Extensions
{
	public static class TriviaExtensions
	{
		public static QuestionPO ToQuestionPO(this TriviaQuestionDTO triviaQuestion)
		{
			var answers = triviaQuestion.IncorrectAnswers.Select(x => new AnswerPO { Answer = WebUtility.HtmlDecode(x) }).ToList();
			answers.Add(new AnswerPO { Answer = WebUtility.HtmlDecode(triviaQuestion.CorrectAnswer), Correct = true });
			answers.Shuffle();

			return new QuestionPO
			{
				Question = WebUtility.HtmlDecode(triviaQuestion.Question),
				Answers = answers
			};
		}
	}
}
