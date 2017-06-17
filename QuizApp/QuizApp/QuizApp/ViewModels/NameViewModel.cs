using MvvmCross.Core.ViewModels;
using QuizApp.Core.Services;
using QuizApp.Core.Utils;

namespace QuizApp.Core.ViewModels
{
	public class NameViewModel : MvxViewModel
	{
		private readonly IUserService _userService;

		public NameViewModel(IUserService userService)
		{
			_userService = userService;

			NextCommand = new MvxCommand(NextAction, IsUserValid);
		}

		private string _user;
		public string User
		{
			get => _user;
			set
			{
				SetProperty(ref _user, value);
				NextCommand.RaiseCanExecuteChanged();
			}
		}

		public IMvxCommand NextCommand { get; }
		private void NextAction()
		{
			_userService.SaveCurrentUser(User);
			ShowViewModel<CategoriesViewModel>();
		}

		private bool IsUserValid()
			=> !string.IsNullOrEmpty(User);
	}
}
