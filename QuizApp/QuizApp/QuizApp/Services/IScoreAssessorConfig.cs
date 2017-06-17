namespace QuizApp.Core.Services
{
	public interface IScoreAssessorConfig
	{
		int MaxTime { get; }
		int BaseReward { get; }
		int RemainingTimeMultiplier { get; }
	}
}