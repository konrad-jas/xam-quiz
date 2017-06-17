namespace QuizApp.Core.Services
{
	public interface IScoreAssessorFactory
	{
		IScoreAssessor GetAssessor();
	}
}
