﻿using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using QuizApp.Core.Services;
using QuizApp.Core.Utils;

namespace QuizApp.Core.ViewModels
{
	public class FinalScoreViewModel : MvxViewModel
	{
		private readonly IHighscoresService _highscoresService;
		private readonly IQuestionsService _questionsService;

		public FinalScoreViewModel(IHighscoresService highscoresService, IQuestionsService questionsService)
		{
			_highscoresService = highscoresService;
			_questionsService = questionsService;

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

		public void Init(int score)
		{
			Score = score;
		}

		public override async void Start()
		{
			await _highscoresService.AddHighscoreAsync(Score, AppData.User);
		}
	}
}
