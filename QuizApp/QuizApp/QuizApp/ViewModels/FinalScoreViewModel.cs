using MvvmCross.Core.ViewModels;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class FinalScoreViewModel : MvxViewModel
	{
		private readonly IHighscoresService _highscoresService;

		public FinalScoreViewModel(IHighscoresService highscoresService)
		{
			_highscoresService = highscoresService;
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

		public override async void Start()
		{
			_highscoresService.AddHighscoreAsync(Score, "test");
		}
	}
}
