using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class FinalScoreViewModel : MvxViewModel
	{
		private readonly IHighscoresService _highscoresService;
		private readonly IQuestionsService _questionsService;
		private readonly IUserService _userService;

		public FinalScoreViewModel(IHighscoresService highscoresService, IQuestionsService questionsService, IUserService userService)
		{
			_highscoresService = highscoresService;
			_questionsService = questionsService;
			_userService = userService;

			RestartCommand = new MvxAsyncCommand(RestartAction);
		}

		public IMvxCommand RestartCommand { get; }
		private async Task RestartAction()
		{
			await _questionsService.WipeMemoryAsync();
			ShowViewModel<CategoriesViewModel>();
		}

		private int _score;
		public int Score
		{
			get => _score;
			set => SetProperty(ref _score, value);
		}

		private bool _madeItToHighscores;
		public bool MadeItToHighscores
		{
			get => _madeItToHighscores;
			set => SetProperty(ref _madeItToHighscores, value);
		}

		private int _place;
		public int Place
		{
			get => _place;
			set => SetProperty(ref _place, value);
		}

		public void Init(int score)
		{
			Score = score;
		}

		public override async void Start()
		{
			(bool inHighscores, int place) = await _highscoresService.QualifiesForHighscores(Score);

			MadeItToHighscores = inHighscores;
			Place = place;

			if (inHighscores)
			{
				await _highscoresService.AddHighscoreAsync(Score, _userService.GetCurrentUser());
			}
		}
	}
}
