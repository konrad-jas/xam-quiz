using MvvmCross.Core.ViewModels;
using QuizApp.Core.Utils;

namespace QuizApp.Core.ViewModels
{
	public class NameViewModel : MvxViewModel
	{
		public NameViewModel()
		{
			NextCommand = new MvxCommand(() => ShowViewModel<CategoriesViewModel>(), () => !string.IsNullOrEmpty(User));
		}

		public IMvxCommand NextCommand { get; }

		private string _user;
		public string User
		{
			get => _user;
			set
			{
				SetProperty(ref _user, value);
				AppData.User = value;
				NextCommand.RaiseCanExecuteChanged();
			}
		}
	}
}
