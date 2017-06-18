using QuizApp.Core.Enums;

namespace QuizApp.Core.Services
{
	public interface IEmotionAnalysisService
	{
		void StartAnalyzing();
		void StopAnalyzing();
		QuestionDifficulty CurrentDifficulty { get; }
	}
}