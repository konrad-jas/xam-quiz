namespace QuizApp.Core.Services.Impl
{
	public class ScoreAssessorFactory : IScoreAssessorFactory
	{
		private readonly IScoreAssessorConfig _config;
		public ScoreAssessorFactory(IScoreAssessorConfig config)
		{
			_config = config;
		}

		public IScoreAssessor GetAssessor()
		{
			return new ScoreAssessor(_config);
		}
	}
}