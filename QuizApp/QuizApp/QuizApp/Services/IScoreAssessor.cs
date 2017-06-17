using System;

namespace QuizApp.Core.Services
{
	public interface IScoreAssessor
	{
		void StartTimer();
		event EventHandler OnTimeRanOut;
		int RemainingSeconds { get; }
		void StopTimer();
		int EvaluateScore();
	}
}
