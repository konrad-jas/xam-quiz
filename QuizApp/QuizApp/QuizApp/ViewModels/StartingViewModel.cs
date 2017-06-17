using MvvmCross.Core.ViewModels;

namespace QuizApp.Core.ViewModels
{
	public class StartingViewModel : MvxViewModel
	{
		public StartingViewModel()
		{
			StartCommand = new MvxCommand(() => ShowViewModel<CategoriesViewModel>());
			HighscoresCommand = new MvxCommand(() => ShowViewModel<HighscoresViewModel>());
		}

		public IMvxCommand StartCommand { get; }
		public IMvxCommand HighscoresCommand { get; }
	}
}
