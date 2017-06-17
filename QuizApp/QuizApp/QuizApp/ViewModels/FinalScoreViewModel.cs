using MvvmCross.Core.ViewModels;

namespace QuizApp.Core.ViewModels
{
	public class FinalScoreViewModel : MvxViewModel
	{
		public FinalScoreViewModel()
		{
			RestartCommand = new MvxCommand(RestartAction);
		}

		public IMvxCommand RestartCommand { get; }

		private void RestartAction()
		{
			ShowViewModel<CategoriesViewModel>();
		}

		private int _score;
		public int Score
		{
			get => _score;
			set => SetProperty(ref _score, value);
		}

		public void Init(int score)
		{
			Score = score;
		}
	}
}
