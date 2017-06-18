using MvvmCross.Core.ViewModels;
using QuizApp.Core.Services;

namespace QuizApp.Core.ViewModels
{
	public class StartingViewModel : MvxViewModel
	{
		private readonly IEmotionAnalysisService _analysisService;
		private bool _analysisEnabled;

		public StartingViewModel(IEmotionAnalysisService analysisService)
		{
			_analysisService = analysisService;
			StartCommand = new MvxCommand(() => ShowViewModel<NameViewModel>());
			HighscoresCommand = new MvxCommand(() => ShowViewModel<HighscoresViewModel>());
			ToggleEmotionAnalysisCommand = new MvxCommand(ToggleEmotionAnalysisAction);
			_analysisEnabled = _analysisService.Enabled;
		}

		public IMvxCommand ToggleEmotionAnalysisCommand { get; set; }
		public IMvxCommand StartCommand { get; }
		public IMvxCommand HighscoresCommand { get; }

		public bool AnalysisEnabled
		{
			get => _analysisEnabled;
			set => SetProperty(ref _analysisEnabled, value);
		}

		private void ToggleEmotionAnalysisAction()
		{
			_analysisService.Enabled = !_analysisService.Enabled;
			AnalysisEnabled = _analysisService.Enabled;
		}
	}
}
