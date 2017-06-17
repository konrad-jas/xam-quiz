namespace QuizApp.Core.Services.Impl
{
	public class ScoreAssessorConfig : IScoreAssessorConfig
	{
		public int MaxTime { get; }
		public int BaseReward { get; }
		public int RemainingTimeMultiplier { get; }

		public ScoreAssessorConfig(int maxTime, int baseReward, int remainingTimeMultiplier)
		{
			MaxTime = maxTime;
			BaseReward = baseReward;
			RemainingTimeMultiplier = remainingTimeMultiplier;
		}	

	}
}