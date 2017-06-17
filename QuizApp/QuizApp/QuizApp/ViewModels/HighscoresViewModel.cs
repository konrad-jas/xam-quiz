using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.POs;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class HighscoresViewModel : MvxViewModel
	{
		private readonly IHighscoresService _highscoresService;

		public HighscoresViewModel(IHighscoresService highscoresService)
		{
			_highscoresService = highscoresService;
		}

		private IEnumerable<ScorePO> _scores;
		public IEnumerable<ScorePO> Scores
		{
			get => _scores;
			set => SetProperty(ref _scores, value);
		}

		public override async void Start()
		{
			Scores = await _highscoresService.GetHighscoresAsync();
		}
	}
}
