namespace QuizApp.Core.Services.Impl
{
	public class UserService : IUserService
	{
		private string _user;

		public string GetCurrentUser()
			=> _user;

		public void SaveCurrentUser(string user)
			=> _user = user;
	}
}
