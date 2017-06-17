namespace QuizApp.Core.Services
{
	public interface IUserService
	{
		string GetCurrentUser();
		void SaveCurrentUser(string user);
	}
}
