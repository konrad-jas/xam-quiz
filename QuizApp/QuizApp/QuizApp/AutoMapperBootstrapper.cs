using System.Net;
using AutoMapper;
using QuizApp.Core.DTOs;
using QuizApp.Core.Models;
using QuizApp.Core.POs;
using QuizApp.Core.Utils;

namespace QuizApp.Core
{
	public static class AutoMapperBootstrapper
	{
		public static void InitializeMappings()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Score, ScorePO>();
				cfg.CreateMap<ScorePO, Score>();

				cfg.CreateMap<TriviaQuestionDTO, QuestionPO>()
					.ForMember(dest => dest.Question, opt => opt.MapFrom(src => WebUtility.HtmlDecode(src.Question)))
					.ForMember(dest => dest.Answers, opt => opt.ResolveUsing<QuestionResolver>());
			});
		}
	}
}
