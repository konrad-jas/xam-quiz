using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using QuizApp.Core.DTOs;
using QuizApp.Core.Extensions;
using QuizApp.Core.POs;

namespace QuizApp.Core.Utils
{
	public class QuestionResolver : IValueResolver<TriviaQuestionDTO, QuestionPO, IEnumerable<AnswerPO>>
	{
		public IEnumerable<AnswerPO> Resolve(TriviaQuestionDTO source, QuestionPO destination, IEnumerable<AnswerPO> destMember,
			ResolutionContext context)
		{
			var answers = source.IncorrectAnswers.Select(x => new AnswerPO { Answer = WebUtility.HtmlDecode(x) }).ToList();
			answers.Add(new AnswerPO { Answer = WebUtility.HtmlDecode(source.CorrectAnswer), Correct = true });
			answers.Shuffle();
			return answers;
		}
	}
}
